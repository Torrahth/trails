using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TorraFramework.Core;


namespace trails.Script;
public class Player : Entity
{
    float zoom = 0.0f;
    public Tile tile_type = TileID.Tile_dirt;
    float gravity = 0;
    int max_gravity = 0;
    int max_horizontal = 0;
    float jump_height = 0;
    float horizontal_movement = 1.5f;
    int KT = 0;
    int horizontal;
    int vertical;
    enum States
    {
        Normal,
        Climbing,
        Float
    }
    States state = States.Normal;
    Entity mouse_cursor_t = new Entity();
    Entity mouse_cursor = new Entity();

    Vector2 player_tile = Vector2.Zero;

    TileMenu menu = new TileMenu();
    public Player(Texture2D _texture)
    {
        Global.players.Add(this);

        texture = _texture;
        mouse_cursor_t.texture = AssetManager.LoadTexture("Cursor", "TileAtlas");
        mouse_cursor.texture = AssetManager.LoadTexture("Cursor", "TileSelector");
        mouse_cursor_t.color = new Color(100, 100, 100, 100);
        GuiManager.AddGui(menu);

        //menu.guis[0].Pressed;

    }
    public override void Update()
    {
         zoom = 0.125f / Camera.GetZoom();
        if (KeyboardManager.KeyJustPressed(Keys.LeftControl))
        {
            if (state == States.Float)
                state = States.Normal;
            else
                state = States.Float;
        }

        KT--;
        gravity = 0.1f;
        max_gravity = 12;
        horizontal_movement = 1.1f;
        max_horizontal = 18;
        jump_height = 1.8f;

        int left =  KeyboardManager.current_state.IsKeyDown(Keys.Left) ? 1 : 0 ;
        int right =  KeyboardManager.current_state.IsKeyDown(Keys.Right) ? 1 : 0 ;

        int up =  KeyboardManager.current_state.IsKeyDown(Keys.Up) ? 1 : 0 ;
        int down =  KeyboardManager.current_state.IsKeyDown(Keys.Down) ? 1 : 0 ;

        horizontal  = left - right;
        vertical = up - down;

        player_tile = new Vector2((int)Math.Floor(Position.X * 0.125f)+1,(int)Math.Floor(Position.Y * 0.125f)+1);
     
        switch (state)
        {
            case States.Normal:
                {
                    Checks();
                    Movement();
                    Position.X += Velocity.X;
                    Position.Y += Velocity.Y ;

                    Collide();
                }
                break;
            case States.Climbing:
                {
                    Checks();
                    Climbing();
                    Position.X += Velocity.X;
                    Position.Y += Velocity.Y ;
                     Collide();
                }
                break;
            case States.Float:
                {
                    Float();
                }
                break;

        }
        Camera.SetPosition(Position);
     
        TileInteractions();
    
        OldPosition = Position;
    }
    private void Checks()
    {
        if (Global.current_world.GetTile((int)player_tile.X, (int)player_tile.Y) == TileID.Tile_Water)
        {
            gravity = 0.02f;
            max_gravity = 1;
            horizontal_movement = 0.5f;
            max_horizontal = 16;
            jump_height = 0.1f;
            KT = 1;
        }
        if (Global.current_world.GetTile((int)player_tile.X, (int)player_tile.Y) == TileID.Sunchain)
        {
            if (vertical == 1)
            {
                state = States.Climbing;
            }

        }
    }
    private void Climbing()
    {
        Velocity = Vector2.Zero;   
        Position.X = player_tile.X * 8 - 4;

        Velocity.Y -= vertical;

        if (horizontal != 0 && vertical == 0 || Global.current_world.GetTile((int)player_tile.X, (int)player_tile.Y) != TileID.Sunchain)
        {
                state = States.Normal;
        }
    }
    private void Movement()
    {
        if (Collisions.W == 0)
        {
            Velocity.Y += gravity;
            if (Velocity.Y > max_gravity)
                Velocity.Y = max_gravity;
        }
        else
        {
            KT = 5;
        }

        if (horizontal != 0)
        {
            Velocity.X -= Math.Clamp(Velocity.X + horizontal_movement * horizontal, -max_horizontal, max_horizontal) ;
        }
        else
        {
             Velocity.X /= 1.5f;
        }
        
        if (vertical == 1 &&  KT > 0)
        {
            Velocity.Y -= jump_height;
            KT = 0;
        }

    }
    private void Collide()
    {
        Collisions = Collision.CheckForTileCollisions(Position.X , Position.Y, 6, 14); //Collision.CheckForTileCollisions(Position.X+ Velocity.X, Position.Y+ Velocity.Y, 7, 14);

        if (Collisions.X != 0 || Collisions.Y != 0)
        {
            //Console.WriteLine( Velocity.X);
            //Console.WriteLine($"{Collisions.X}, {Collisions.Y}");
            // new Vector2((int)Math.Floor(Position.X * 0.125f)+1,(int)Math.Floor(Position.Y * 0.125f)+1)
           //Position.X = OldPosition.X  ;
            Position.X -= Collisions.X;// * 0.01f;
            Position.X += Collisions.Y;///* 0.01f;
            Velocity.X = 0;
        
        }
           
        if (Collisions.W != 0 || Collisions.Z != 0)
        {
            //Position.Y = OldPosition.Y;
            Position.Y -= Collisions.W;
            Position.Y += Collisions.Z;
            Velocity.Y = 0;
           
        }

        
    }
    private void TileInteractions()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.D1))
            tile_type = TileID.Tile_dirt;
        if (Keyboard.GetState().IsKeyDown(Keys.D2))
            tile_type = TileID.Tile_crystal;
        if (Keyboard.GetState().IsKeyDown(Keys.D3))
            tile_type = TileID.Tile_brick;
        if (Keyboard.GetState().IsKeyDown(Keys.D4))
            tile_type = TileID.Tile_Water;
        if (Keyboard.GetState().IsKeyDown(Keys.D5))
            tile_type = TileID.Sunstone;
        if (Keyboard.GetState().IsKeyDown(Keys.D6))
            tile_type = TileID.Sunchain;
        if (Keyboard.GetState().IsKeyDown(Keys.D7))
            tile_type = TileID.Fractal;
        if (Keyboard.GetState().IsKeyDown(Keys.D8))
            tile_type = TileID.PurpleMetal;
        if (Keyboard.GetState().IsKeyDown(Keys.D9))
            tile_type = TileID.InnermostFractal;
        if (Keyboard.GetState().IsKeyDown(Keys.D0))
            tile_type = TileID.BloodOrb;
        if (Keyboard.GetState().IsKeyDown(Keys.Q))
            tile_type = TileID.RoseQuartz;

        Vector2 mouse_pos = Mouse.GetState().Position.ToVector2() * zoom;
        Vector2 worldhalf = (new Vector2(Global.current_world._sizeX, Global.current_world._sizeY) * 0.5f) ;
        Vector2 pos = ((Position * 0.125f) + mouse_pos - (Camera.GetHalfViewport()* zoom))  + worldhalf; //((Mouse.GetState().Position.ToVector2() + new Vector2(-Camera.GetHalfViewport().X, Camera.GetHalfViewport().Y)) / 8) + Position / 8;//     * Camera.GetZoom()) + ( Camera.GetPosition() / 8) ;
        int player_tile_x = (int)(pos.X )+1 ; //((int)Position.X  / 8)+ (Global.current_world._sizeX/2);
        int player_tile_y = (int)(pos.Y )+1; //((int)Position.Y/ 8) +(Global.current_world._sizeY/2);
        
  
        mouse_cursor_t.draw_rectangle = tile_type.texture_bounds;
        mouse_cursor_t.Position = (new Vector2(player_tile_x-0.5f, player_tile_y-0.5f) - worldhalf) * 8; // + new Vector2(Global.current_world._sizeX, Global.current_world._sizeY) * 0.5f;
        mouse_cursor.Position = mouse_cursor_t.Position;
        //AssetManager.QuickDraw(mouse_cursor_t.Position.X, mouse_cursor_t.Position.Y, AssetManager.LoadTexture("glow", "SmallLight"));
        if (Mouse.GetState().LeftButton == ButtonState.Pressed && !Main.MouseOverGui && TouchingPlayer(player_tile_x, player_tile_y, worldhalf) )
        {
            Global.current_world.SetTile(player_tile_x, player_tile_y, tile_type);
            Console.WriteLine($"{(int)worldhalf.X}, {(int)worldhalf.Y}");
         }
        if (Mouse.GetState().RightButton == ButtonState.Pressed && !Main.MouseOverGui && TouchingPlayer(player_tile_x, player_tile_y, worldhalf) )
        {
             Global.current_world.SetTile(player_tile_x, player_tile_y, TileID.Tile_air);
            Console.WriteLine($"{(int)player_tile_x}, {(int)player_tile_y}");
        }   
    }
    private bool TouchingPlayer(int player_tile_x, int player_tile_y, Vector2 worldhalf)
    {
        int eX = (int)Math.Ceiling(player_tile.X-0.5f) ;
        int eY = (int)Math.Ceiling(player_tile.Y-0.5f);

        if (Global.current_world.GetTile(eX, eY).Collidable==true||Global.current_world.GetTile(eX, eY-1).Collidable==true )
            return false;
     
       // AssetManager.QuickDraw(((int)Math.Ceiling(player_tile.X-0.5f) *8)-4f, ((int)Math.Ceiling(player_tile.Y-0.5f)*8)-4f, 8, 8, Color.Aqua);
        return new Vector2((int)Math.Ceiling(player_tile_x-0.5f), (int)Math.Ceiling(player_tile_y-0.5f)) != player_tile+worldhalf && new Vector2((int)Math.Ceiling(player_tile_x-0.5f), (int)Math.Ceiling(player_tile_y+0.5f)) != player_tile+worldhalf;
    }
    public void Float()
    {
        if (KeyboardManager.current_state.IsKeyDown(Keys.LeftShift))
        {
            Position.X -= horizontal * 12;
            Position.Y -= vertical * 12;
        }
        else
        {
            Position.X -= horizontal * 4;
            Position.Y -= vertical * 4;
        }

    }
}