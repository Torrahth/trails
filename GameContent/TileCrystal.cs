using Microsoft.Xna.Framework;
using trails.Script;

namespace trails.GameContent;

public  class TileCrystal : Tile
{
    public TileCrystal()
    {
        tile_id = 2;
        texture_bounds = new Rectangle(16, 0, 8, 8);
    }
   
    
}