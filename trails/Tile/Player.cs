using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace trails.Script;
public class Player : Object
{
        float c_scale;

        Vector2 mouse_pos;
        Vector2 camera_pos;
        Vector2 offset;
        Vector2 WorldSize;

        Vector2 pos;
    public Player(Texture2D _texture)
    {
        texture = _texture;
        WorldSize = new Vector2(Global.current_world._sizeX, Global.current_world._sizeY);
    }
    public override void Update()
    {

        int left =  Keyboard.GetState().IsKeyDown(Keys.Left) ? 1 : 0 ;
        int right =  Keyboard.GetState().IsKeyDown(Keys.Right) ? 1 : 0 ;

        int up =  Keyboard.GetState().IsKeyDown(Keys.Up) ? 1 : 0 ;
        int down =  Keyboard.GetState().IsKeyDown(Keys.Down) ? 1 : 0 ;

        int horizontal  = left - right;
        int vertical = up - down;

        Position.X -= horizontal;
        Position.Y -= vertical ;

        Camera.SetPosition(Position);

        TileManagement();
  

        var collide = Collision.TileCollide(Position, 6, 12);
        if (collide.X == 1 || collide.Y == 1)
        {
            Position.X = old_position.X;
        }
        if (collide.W == 1 || collide.Z == 1)
        {
            Position.Y = old_position.Y;
        }

        old_position = Position;
    }
    protected void TileManagement()
    {
        c_scale = 0.125f / Camera.GetZoom();

        mouse_pos = Mouse.GetState().Position.ToVector2() * c_scale;
        camera_pos = Camera.GetPosition() * 0.125f;
        offset = Camera.GetViewport()  * c_scale;

        pos = mouse_pos + camera_pos - offset + WorldSize * 0.5f;

        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            Global.current_world.SetTile((int)pos.X + 1,(int)pos.Y + 1, TileID.Tile_air);
    }
}