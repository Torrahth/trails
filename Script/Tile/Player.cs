using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TorraFramework.Core;


namespace trails.Script;
public class Player : Object
{
    float zoom = 0.0f;
    public Player(Texture2D _texture)
    {
        texture = _texture;
    }
    public override void Update()
    {

        int left =  Keyboard.GetState().IsKeyDown(Keys.Left) ? 1 : 0 ;
        int right =  Keyboard.GetState().IsKeyDown(Keys.Right) ? 1 : 0 ;

        int up =  Keyboard.GetState().IsKeyDown(Keys.Up) ? 1 : 0 ;
        int down =  Keyboard.GetState().IsKeyDown(Keys.Down) ? 1 : 0 ;

        int horizontal  = left - right;
        int vertical = up - down;


        if (Collisions.Z == 0)
        {
            Velocity.Y += 0.1f;
        }

        Velocity.X -= 0.5f * horizontal ;
        if (up != 0 && Collisions.Z == 1)
        {
            Velocity.Y -= 3;
        }
        //Position.Y -= vertical  ;

        Velocity.X /= 1.4f;
    
      

        Position += Velocity;



         Collide();
        Camera.SetPosition(Position);

       

        zoom = 8 * Camera.GetZoom();

        Vector2 mouse_pos = Mouse.GetState().Position.ToVector2() / zoom;
        Vector2 worldhalf = (new Vector2(Global.current_world._sizeX, Global.current_world._sizeY) * 0.5f) ;
        Vector2 pos = (Position * 0.125f + mouse_pos  - Camera.GetHalfViewport()/ zoom)  + worldhalf; //((Mouse.GetState().Position.ToVector2() + new Vector2(-Camera.GetHalfViewport().X, Camera.GetHalfViewport().Y)) / 8) + Position / 8;//     * Camera.GetZoom()) + ( Camera.GetPosition() / 8) ;
        int player_tile_x = (int)pos.X +1 ; //((int)Position.X  / 8)+ (Global.current_world._sizeX/2);
        int player_tile_y = (int)pos.Y +1; //((int)Position.Y/ 8) +(Global.current_world._sizeY/2);

        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            Global.current_world.SetTile(player_tile_x, player_tile_y, TileID.Tile_brick);
        
        if (Mouse.GetState().RightButton == ButtonState.Pressed)
            Global.current_world.SetTile(player_tile_x, player_tile_y, TileID.Tile_air);
        OldPosition = Position;
    }
    private void Collide()
    {
        Collisions = Collision.CheckForTileCollisions(Position.X, Position.Y, 7, 14);

        if (Collisions.X == 1)
        {
            Velocity.X = 0;
            Position.X = OldPosition.X;
        }
        if (Collisions.Y == 1)
        {
            Velocity.X = 0;
            Position.X = OldPosition.X;

        }   
        if (Collisions.W == 1)
        {
            Velocity.Y = 0;
            Position.Y = OldPosition.Y;
        }    
        if (Collisions.Z == 1)
        {
            Velocity.Y = 0;
            Position.Y = OldPosition.Y;
        }   
    
    }
  
}