using Microsoft.Xna.Framework;
using trails.Script;

namespace trails.GameContent;

public  class TileBrick : Tile
{
    public TileBrick()
    {
        tile_id = 3;
        texture_bounds = new Rectangle(24, 0, 8, 8);
        Collidable = true;

    }
   
    
}