using Microsoft.Xna.Framework;
using trails.Script;

namespace trails.GameContent;
public  class TileAir : Tile
{
    public TileAir() : base(0, 0, collidable: false)
    {
        tile_id = 0;
    }
}
public  class TileDirt : Tile
{
    public TileDirt() : base(1, 0)
    {
        tile_id = 1;
    }
}
public  class TileCrystal : Tile
{
    public TileCrystal() : base(2, 0)
    {
        tile_id = 2;
    }
}
public  class TileBrick : Tile
{
    public TileBrick(): base(3, 0)
    {
        tile_id = 3;
    }
}
public  class TileWater : Tile
{
    public TileWater(): base(0, 1)
    {
        tile_id = 4;
        Collidable = false;
    }
}
public  class TileSunStone : Tile
{
    public TileSunStone(): base(1, 1)
    {
        tile_id = 5;
    }
}
public  class TileSunChain : Tile
{
    public TileSunChain(): base(2, 1)
    {
        tile_id = 6;
        Collidable = false;
    }
}
public  class TileFractal : Tile
{
    public TileFractal(): base(3, 1)
    {
        tile_id = 7;
    }
}
public  class TileInnermostFractal : Tile
{
    public TileInnermostFractal(): base(3, 2)
    {
        tile_id = 8;
        Collidable = false;
    }
}
public  class TilePurpleMetal : Tile
{
    public TilePurpleMetal(): base(0, 2)
    {
        tile_id = 9;
    }
}
public class TileBloodOrb : Tile
{
    public TileBloodOrb(): base(2, 2)
    {
        tile_id = 10;
    }
}
public class TileRoseQuartz : Tile
{
    public TileRoseQuartz(): base(3, 2)
    {
        tile_id = 11;
    }
}