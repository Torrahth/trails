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
    Tile tile_type = TileID.Tile_dirt;
    float gravity = 0;
    int max_gravity = 0;
    int max_horizontal = 0;
    float jump_height = 0;
    float horizontal_movement = 1.5f;
    int KT = 0;
    enum States
    {
        Normal,
        Climbing
    }
    States state = States.Normal;
    Entity mouse_cursor = new Entity();
    Vector2 player_tile = Vector2.Zero;
    public Player(Texture2D _texture)
    {
        texture = _texture;
        mouse_cursor.texture = AssetManager.LoadTexture("Cursor", "TileSelector");
    }
    public override void Update()
    {
        KT--;
        gravity = 0.1f;
        max_gravity = 12;
        horizontal_movement = 1.5f;
        max_horizontal = 18;
        jump_height = 1.8f;

        int left =  Keyboard.GetState().IsKeyDown(Keys.Left) ? 1 : 0 ;
        int right =  Keyboard.GetState().IsKeyDown(Keys.Right) ? 1 : 0 ;

        int up =  Keyboard.GetState().IsKeyDown(Keys.Up) ? 1 : 0 ;
        int down =  Keyboard.GetState().IsKeyDown(Keys.Down) ? 1 : 0 ;

        int horizontal  = left - right;
        int vertical = up - down;

       

        player_tile = new Vector2((int)Math.Floor(Position.X * 0.125f)+1,(int)Math.Floor(Position.Y * 0.125f)+1);
        mouse_cursor.Position = new Vector2((int)Math.Floor(  player_tile.X* 8f), (int)Math.Floor( player_tile.Y* 8f)); // + new Vector2(Global.current_world._sizeX, Global.current_world._sizeY) * 0.5f;
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
         Collide();
        switch (state)
        {
            case States.Normal:
                {
                    Movement(horizontal, vertical);
                }
                break;
            case States.Climbing:
                {
                    Climbing(horizontal, vertical);
                }
                break;

        }
         
            Camera.SetPosition(Position);
     
      

        
    
        TileInteractions();
    
        OldPosition = Position;
    }
    private void Climbing(int horizontal, int vertical)
    {
        Velocity = Vector2.Zero;   
        Position.X = player_tile.X * 8 - 4;

        Velocity.Y -= vertical;

        if (horizontal != 0 || Global.current_world.GetTile((int)player_tile.X, (int)player_tile.Y) != TileID.Sunchain)
        {
                state = States.Normal;
        }
    }
    private void Movement(int horizontal, int vertical)
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
            // Velocity.X /= 1.1f;
        }
        
        if (vertical == 1 &&  KT > 0)
        {
            Velocity.Y -= jump_height;
            KT = 0;
        }

    }
    private void Collide()
    {
        Collisions = Collision.CheckForTileCollisions(Position.X , Position.Y+ Velocity.Y, 6, 14); //Collision.CheckForTileCollisions(Position.X+ Velocity.X, Position.Y+ Velocity.Y, 7, 14);

        if (Collisions.X != 0 || Collisions.Y != 0)
        {
            var x = Collisions.X;//(float) MathF.Round(Collisions.X, 2);
            var y = Collisions.Y;// (float) MathF.Round(Collisions.Y, 2);
               Console.WriteLine( Velocity.X);
            Console.WriteLine($"{x}, {y}");
            // new Vector2((int)Math.Floor(Position.X * 0.125f)+1,(int)Math.Floor(Position.Y * 0.125f)+1)
           //Position.X = OldPosition.X  ;
            Position.X += x ;// * 0.01f;
            Position.X += y;///* 0.01f;
            Velocity.X = 0;
        
        }
        else
        {
            Position.X += Velocity.X;
        }
        if (Collisions.W != 0 || Collisions.Z != 0)
        {
            Position.Y = OldPosition.Y;
         //   Position.Y -= Collisions.W* 0.010f;
         //   Position.Y += Collisions.Z* 0.010f;
            Velocity.Y = 0;
           
        }
        else
        {
            Position.Y += Velocity.Y;
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
        zoom = 8 * Camera.GetZoom();

        Vector2 mouse_pos = Mouse.GetState().Position.ToVector2() / zoom;
        Vector2 worldhalf = (new Vector2(Global.current_world._sizeX, Global.current_world._sizeY) * 0.5f) ;
        Vector2 pos = (Position * 0.125f + mouse_pos  - Camera.GetHalfViewport()/ zoom)  + worldhalf; //((Mouse.GetState().Position.ToVector2() + new Vector2(-Camera.GetHalfViewport().X, Camera.GetHalfViewport().Y)) / 8) + Position / 8;//     * Camera.GetZoom()) + ( Camera.GetPosition() / 8) ;
        int player_tile_x = (int)pos.X +1 ; //((int)Position.X  / 8)+ (Global.current_world._sizeX/2);
        int player_tile_y = (int)pos.Y +1; //((int)Position.Y/ 8) +(Global.current_world._sizeY/2);

        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            Global.current_world.SetTile(player_tile_x, player_tile_y, tile_type);
        
        if (Mouse.GetState().RightButton == ButtonState.Pressed)
            Global.current_world.SetTile(player_tile_x, player_tile_y, TileID.Tile_air);
    }
  
}