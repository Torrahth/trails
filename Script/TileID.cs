using System.Collections.Generic;
using trails.GameContent;

namespace trails.Script;

public static class TileID
{
    public static Tile Tile_air = new TileAir();
    public static Tile Tile_dirt = new TileDirt();
    public static Tile Tile_crystal = new TileCrystal();
    public static Tile Tile_brick = new TileBrick();
    public static Tile Tile_Water = new TileWater();
    public static Tile Sunstone = new TileSunStone();
    public static Tile Sunchain = new TileSunChain();
    public static Tile Fractal = new TileFractal();
    public static Tile InnermostFractal = new TileInnermostFractal();
    public static Tile PurpleMetal = new TilePurpleMetal();
    public static Tile BloodOrb = new TileBloodOrb();

    public static Tile RoseQuartz = new TileRoseQuartz();
    public static Tile Stone = new TileStone();
    public static Tile RedSand = new TileRedSand();
    public static Tile Sand = new TileSand();
    public static Tile JungleSoil = new TileJungleSoil();
    public static List<Tile> tiles = new List<Tile>();
    static TileID(){
        tiles.Add(Tile_dirt);
        tiles.Add(Tile_crystal);
        tiles.Add(Tile_brick);
        tiles.Add(Tile_Water);
        tiles.Add(Sunstone);
        tiles.Add(Sunchain);
        tiles.Add(Fractal);
        tiles.Add(InnermostFractal);
        tiles.Add(PurpleMetal);
        tiles.Add(BloodOrb);
        tiles.Add(RoseQuartz);
        tiles.Add(Stone);
         tiles.Add(RedSand);
          tiles.Add(Sand);
           tiles.Add(JungleSoil);
        // convert tile files to like: 1TileDirt and shi.. wait htat wont work because stuff like 13.. uhh
    }

}