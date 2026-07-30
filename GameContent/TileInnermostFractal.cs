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

public class TileBloodOrb : Tile
{
    public TileBloodOrb()
    {
        tile_id = 10;
        texture_bounds = new Rectangle(16, 16, 8, 8);
    }
}