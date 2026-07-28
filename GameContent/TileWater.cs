using Microsoft.Xna.Framework;
using trails.Script;

namespace trails.GameContent;

public  class TileWater : Tile
{
    public TileWater()
    {
        tile_id = 4;
        texture_bounds = new Rectangle(0, 8, 8, 8);
        Collidable = false;
    }
   
    
}