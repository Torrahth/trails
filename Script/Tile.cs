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
    public Tile(int x, int y, int sizex=8, int sizey=8, bool collidable=true)
    {
        texture_bounds.X = ((x * 8) *4)+8;
        texture_bounds.Y = ((y * 8)*4 )+8;
        texture_bounds.Width = sizex;
        texture_bounds.Height = sizey;
        Collidable=collidable;
    }
    public static Tile ConvertToTile(int x, int y)
    {
        Vector2 search = new Vector2(x, y);
        //TileID.tiles.
        foreach (Tile tile in TileID.tiles)
        {
            //((x * 8) *4)+8
            Console.WriteLine(new Vector2((int)Math.Floor(tile.texture_bounds.Location.X* 0.125f * 0.25f), (int)Math.Floor(tile.texture_bounds.Location.Y* 0.125f * 0.25f)));
            if (search == new Vector2((int)Math.Floor(tile.texture_bounds.Location.X* 0.125f * 0.25f), (int)Math.Floor(tile.texture_bounds.Location.Y* 0.125f * 0.25f)))
            {
                return tile;
            }
        }
        return TileID.Stone;
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
