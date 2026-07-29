using Microsoft.Xna.Framework;
using trails.Script;

namespace trails.GameContent;

public  class TileInnermostFractal : Tile
{
    public TileInnermostFractal()
    {
        tile_id = 8;
        texture_bounds = new Rectangle(24, 16, 8, 8);
        Collidable = false;
    }
   
    
}