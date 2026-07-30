using Microsoft.Xna.Framework;
using trails.Script;

namespace trails.GameContent;

public  class TilePurpleMetal : Tile
{
    public TilePurpleMetal()
    {
        tile_id = 9;
        texture_bounds = new Rectangle(0, 16, 8, 8);
    }
}