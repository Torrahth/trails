using Microsoft.Xna.Framework;
using trails.Script;

namespace trails.GameContent;

public  class TileAir : Tile
{
    public TileAir()
    {
        tile_id = 0;
        texture_bounds = new Rectangle(0, 0, 8, 8);
    }
   
    
}