using System;
using TorraFramework.Core;

namespace trails.Script;

public class WorldGenTypes
{
    World world;
    public WorldGenTypes(World world, int type){
        this.world = world;

        switch (type)
        {
            case 0: // Crazy world
                {
                    GenerateCrazyWorld();
                }
                break;
            case 1: // Fractal world
                {
                    GenerateFractalWorld();
                }
                break;
            case 2: // Normal world
                {
                    GeneratePlains();
                }
                break;
            case 3: // secret MURPLES
                {
                    GenerateCellWorld();    
                }
                break;

        }
    }

    public void GeneratePlains()
    {
        world.sky_color = new Microsoft.Xna.Framework.Color(225, 232, 255);

        Random rng = new Random();
        int slope = (int)(world._sizeY * 0.51f);
        int mountain_steps = 0;
        int half_mountain_steps = 0;
        int mountain_steepness = 0;
        for (int X = 0; X < world._sizeX; ++X)
        {
       

            if (mountain_steps > 0)
            {
                if (mountain_steps < half_mountain_steps)
                {
                    if (mountain_steepness > 0)
                         slope += rng.Next(1, mountain_steepness);
                    else
                         slope += rng.Next(mountain_steepness-1, -1);
                   
                  
                }
                else
                {
                    if (mountain_steepness > 0)
                        slope -= rng.Next(1, mountain_steepness);
                    else
                         slope -= rng.Next(mountain_steepness-1, -1);
                }
                mountain_steps--; 
                if (mountain_steps == 0)
                    half_mountain_steps = 0;
            }
            else if (rng.Next(0, 92) == 0 && Main.DistanceFrom((int)(world._sizeX * 0.5f), X) > 100)
            {
                mountain_steepness= rng.Next(-4, 4);
                mountain_steps = mountain_steepness > 0 ? rng.Next(50, 80) : rng.Next(19, 40);
                half_mountain_steps = (int)(mountain_steps * 0.5f);
               
            }
            else
            {
                if (rng.Next(0, 3) == 0)
                     slope = Math.Clamp(slope + rng.Next(-1, 2), (int)(world._sizeY * 0.45f), (int)(world._sizeY * 0.55f));

             
            }

            for (int Y = 0; Y < world._sizeY; Y++)
            {
                

                 if (Y > slope)
                {
                    world.SetTile(X, Y, TileID.Tile_dirt);
                }
        
             

            }
          
          
        }
        for (int X = 0; X < world._sizeX; ++X)
        {
            for (int Y = 0; Y < world._sizeY; Y++)
            {
                if (rng.Next(0, 2533) == 0)
                {
                    int size = rng.Next(10, 30);
                    world.GenerateOrb(X, Y, TileID.PurpleMetal, size);
                    world.GenerateOrb(X, Y, TileID.Tile_air, size - 2);
                    world.GenerateFissure(X, Y, rng, TileID.Tile_crystal, rng.Next(5, 10) * size);


                }
            }
        }
        world.GenerateRectangle((int)(world._sizeX*0.5f), (int)(world._sizeY*0.5f) + 2, TileID.Tile_dirt, 4, 1);

        world.GenerateWorldBorder(TileID.Fractal,  2);
    }
    public void GenerateFractalWorld()
    {
        world.sky_color = new Microsoft.Xna.Framework.Color(125, 232, 255);

        Random rng = new Random();
        for (int X = 0; X < world._sizeX; ++X)
        {
            for (int Y = 0; Y < world._sizeY; Y++)
            {
                

             

                if (rng.Next(0, 1552) == 0)
                  world.GenerateFissure(X, Y, rng, TileID.Tile_Water, rng.Next(400, 1000));
                else if (rng.Next(0, 1552) == 0)
                {
                        world.GenerateFissure(X, Y, rng, TileID.InnermostFractal, rng.Next(400, 1000));
                }
                else if (rng.Next(0, 1552) == 0)
                   world.GenerateFissure(X, Y, rng, TileID.Fractal, rng.Next(400, 1000));

            }
        }
        world.GenerateRectangle((int)(world._sizeX*0.5f), (int)(world._sizeY*0.5f) + 2, TileID.Tile_dirt, 4, 1);

        world.GenerateWorldBorder(TileID.Fractal,  2);
    }
    public void GenerateCrazyWorld()
    {
        world.sky_color = new Microsoft.Xna.Framework.Color(225, 232, 255);
         Random rng = new Random();

        for (int X = 0; X < world._sizeX; ++X)
        {
            for (int Y = 0; Y < world._sizeY; Y++)
            {

               
               
                if (Y < world._sizeY * 0.33f)
                {
                    if (rng.Next(0, 2) == 0)
                      world.SetTile(X, Y, TileID.Tile_dirt);
                    else
                        world.SetTile(X, Y, TileID.Tile_air);
                    
                    if (X < world._sizeX * 0.5f && rng.Next(0, 2) == 0)
                    {
                        world.SetTile(X, Y, TileID.Tile_crystal);
                       
                    }

                }
                else if (Y > world._sizeY * 0.75f)
                {
                    if (X < world._sizeX * 0.5f)
                    {
                    if (rng.Next(0, 136) == 0)
                        world.GenerateRectangle(X, Y, TileID.Tile_brick, rng.Next(5, 22) , rng.Next(1, 3));
                    if (rng.Next(0, 136) == 0)
                        world.GenerateRectangle(X, Y - 4, TileID.Sunchain, 1, rng.Next(1, 22));
                    if (rng.Next(0, 136) == 0)
                        world.GenerateRectangle(X, Y, TileID.Sunstone, 1 , 1);
                    }
                    else
                    {
                         world.SetTile(X, Y, TileID.Tile_Water);
                   
                           
                    }
                }
                if (rng.Next(0, 236) == 0)
                        world.GenerateRectangle(X, Y, TileID.Tile_dirt, rng.Next(1, 3) , rng.Next(1, 3));
                if (X > world._sizeX * 0.90)
                    if (rng.Next(0, 1552) == 0)
                         world.GenerateFissure(X, Y, rng, TileID.Tile_Water, rng.Next(400, 1000));
                    else if (rng.Next(0, 1552) == 0)
                    {
                        world.GenerateFissure(X, Y, rng, TileID.InnermostFractal, rng.Next(400, 1000));
                    }
                else if (X > world._sizeX * 0.87f && rng.Next(0, 1552) == 0)
                  world.GenerateFissure(X, Y, rng, TileID.Fractal, rng.Next(400, 1000));
            } 
            
        }
        for (int X = 0; X < world._sizeX; ++X)
        {
            for (int Y = 0; Y < world._sizeY; Y++)
            {
                 if ( Y == world._sizeY - 1&&X > world._sizeX * 0.5f  && rng.Next(0, 4) == 0  )
                 {
                     int offset = (int)rng.Next(1, (int)(world._sizeY * 0.25f) - 10);
                            
                      world.GenerateSeaWeed(X , Y - (int)(offset*0.5f), TileID.Tile_dirt, 1, offset, 2);
                }
            }
        }
        world.GenerateRectangle((int)(world._sizeX * 0.5f), (int)(world._sizeY * 0.5f), TileID.Tile_crystal, 3 * (world._sizeX * 0.02f), 3* (world._sizeX * 0.02f));
        world.GenerateRectangle((int)(world._sizeX * 0.5f), (int)(world._sizeY * 0.5f), TileID.Tile_Water, 1 * (world._sizeX * 0.02f), 1* (world._sizeX * 0.02f));

         world.GenerateWorldBorder(TileID.Tile_brick,  2);
    }
     public void GenerateCellWorld()
    {
        world.sky_color = new Microsoft.Xna.Framework.Color(25, 8, 8);
        Random rng = new Random();


        for (int X = 0; X < world._sizeX; ++X)
        {
            for (int Y = 0; Y < world._sizeY; Y++)
            {
                if (rng.Next(0, 1000) == 0)
                {

                    world.GenerateFissure(X, Y, rng, TileID.InnermostFractal, rng.Next(444, 1000) );


                }
            }
        }


        for (int X = 0; X < world._sizeX; ++X)
        {
            for (int Y = 0; Y < world._sizeY; Y++)
            {
                if (X % 20 == 19 && Y % 20 == 19 && rng.Next(0, 1) == 0)
                {
                
                        int size = rng.Next(6, 15);
                        if (rng.Next(0, 1) == 0)
                        world.GenerateFissure(X, Y- (int)(size*0.5f) + 1, rng, TileID.Fractal, size );
                          world.GenerateOrb(X, Y, TileID.BloodOrb, size);
 


                }
            }
        }

        int rX = rng.Next(120, world._sizeX - 120);
        int rY = rng.Next(120, world._sizeY - 120);
        world.GenerateFissure(rX, rY, rng, TileID.Fractal, 900 );
        world.GenerateFissure(rX, rY, rng, TileID.Fractal, 900 );

        world.GenerateFissure(rX, rY, rng, TileID.Fractal, 900 );

        world.GenerateOrb(rX, rY, TileID.BloodOrb, 30);

        world.GenerateOrb(rX, rY, TileID.Tile_Water, 25);
        world.GenerateFissure(rX, rY, rng, TileID.BloodOrb, 5 );

        world.GenerateWorldBorder(TileID.Fractal,  2);


    }
    public void GenerateRoom(int length)
    {
        
    }
}