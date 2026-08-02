using Microsoft.Xna.Framework;
using trails.Script;

namespace trails.GameContent;

public  class TileDirt : Tile
{
    public TileDirt()
    {
        tile_id = 1;
        texture_bounds = new Rectangle(8, 0, 8, 8);
        Collidable = true;

    }
   
    
}