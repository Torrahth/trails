using System;
using System.Runtime.Intrinsics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace trails.Script;


public class Tile
{
    public int tile_id;
    public Rectangle texture_bounds;
    public bool Collidable;
    //public Texture2D texture;

    public void Init(Texture2D new_texture)
    {
        Collidable = true;

       // texture = new_texture;
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
