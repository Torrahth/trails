using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace trails.Script;
public class Player : Object
{
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

        Position.X -= horizontal * 2;
        Position.Y -= vertical * 2;

        Camera.SetPosition(Position);

        int player_tile_x = ((int)Position.X  / 8)+ (Global.current_world._sizeX/2);
         int player_tile_y = ((int)Position.Y/ 8) +(Global.current_world._sizeY/2);

        Global.current_world.SetTile(player_tile_x, player_tile_y, TileID.Tile_brick);
    }
  
}