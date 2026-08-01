using System;
using System.Runtime.Intrinsics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace trails.Script;


public class Tile
{
    public int tile_id;
    public int sizeX;
    public int sizeY;
    public Rectangle texture_bounds = new Rectangle(0, 0, 8, 8);
    public bool Collidable;
    //public Texture2D texture;

    public void Init(Texture2D new_texture)
    {


       // texture = new_texture;
    }
    public Tile(int x, int y, int sizex=1, int sizey=1, bool collidable=true)
    {
        texture_bounds.X = ((x * 8) + 1)*4;
        texture_bounds.Y = ((y * 8) + 1)*4;
        texture_bounds.Width = sizex*8;
        texture_bounds.Height = sizey*8;
        Collidable=collidable;
    }
    public void GetTileSize() // b
    {
        Vector2 tile_size = Divide();
        tile_size *= 8;
    }
    public Vector2 Divide() //get tile size in world tiles
    {
        float XTiles = texture_bounds.Size.X / 8;
        float YTiles = texture_bounds.Size.Y / 8;

        XTiles = (float)Math.Floor(XTiles);
        YTiles = (float)Math.Floor(YTiles);

        return new Vector2(XTiles, YTiles);
    }

}
