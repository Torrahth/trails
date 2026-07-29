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
    float horizontal_movement = 1.5f;
    Entity mouse_cursor = new Entity();
    public Player(Texture2D _texture)
    {
        texture = _texture;
        mouse_cursor.texture = AssetManager.LoadTexture("Cursor", "TileSelector");
    }
    public override void Update()
    {
        gravity = 0.1f;
        horizontal_movement = 1.5f;

        int left =  Keyboard.GetState().IsKeyDown(Keys.Left) ? 1 : 0 ;
        int right =  Keyboard.GetState().IsKeyDown(Keys.Right) ? 1 : 0 ;

        int up =  Keyboard.GetState().IsKeyDown(Keys.Up) ? 1 : 0 ;
        int down =  Keyboard.GetState().IsKeyDown(Keys.Down) ? 1 : 0 ;

        int horizontal  = left - right;
        int vertical = up - down;

        

        Vector2 mp = new Vector2((int)Math.Floor(Mouse.GetState().Position.X * 0.125f),(int)Math.Floor(Mouse.GetState().Position.Y * 0.125f));
        mouse_cursor.Position = new Vector2((int)Math.Floor(  mp.X), (int)Math.Floor( mp.Y)); // + new Vector2(Global.current_world._sizeX, Global.current_world._sizeY) * 0.5f;
        if (Global.current_world.GetTile((int)mp.X, (int)mp.Y) == TileID.Tile_Water)
        {
            gravity = 0.02f;
            horizontal_movement = 0.5f;
        }

        if (Collisions.Z == 0)
        {
            Velocity.Y += gravity;
        }

        if (horizontal != 0)
        {
            Velocity.X -= Math.Clamp(Velocity.X + horizontal_movement * horizontal, -18, 18) ;
        }
        else
        {
             Velocity.X /= 1.4f;
        }
        
        if (up != 0 && Collisions.Z == 1)
        {
            Velocity.Y -= 1.8f;
        }

      

        Collide();

        
        Camera.SetPosition(Position);
        TileInteractions();
    
        OldPosition = Position;
    }
    private void Collide()
    {
        Collisions = Collision.CheckForTileCollisions(Position.X+ Velocity.X, Position.Y+ Velocity.Y, 7, 14);

        if (Collisions.X == 1 || Collisions.Y == 1)
        {
            
            //Position.X += Collisions.X;
            //Position.X -= Collisions.Y;
            Velocity.X = 0;
            Position.X = OldPosition.X ;
        }
        else
        {
            Position.X += Velocity.X;
        }
        if (Collisions.W == 1 || Collisions.Z == 1)
        {
           
           // Position.Y += Collisions.W;
           // Position.Y -= Collisions.Z;
            Velocity.Y = 0;
            Position.Y = OldPosition.Y;
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