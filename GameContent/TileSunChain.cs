using Microsoft.Xna.Framework;
using trails.Script;

namespace trails.GameContent;

public  class TileSunChain : Tile
{
    public TileSunChain()
    {
        tile_id = 6;
        texture_bounds = new Rectangle(16, 8, 8, 8);
        Collidable = false;
    }
   
    
}