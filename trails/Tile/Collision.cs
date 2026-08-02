using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TorraFramework.Core;

namespace trails.Script;

public static class Collision
{
    static List<Rectangle>  Layer1 = new List<Rectangle>();
    static List<Rectangle> Layer2 = new List<Rectangle>();

    static int worldX = (int)(Global.current_world._sizeX * 0.5f);
    static int worldY =  (int)(Global.current_world._sizeY * 0.5f);
    public static SpriteBatch spriteBatch;
    /// <summary> Checks surrounding tiles
    ///
    /// </summary>
    /// <param name="position"></param>
    /// <param name="size_x"></param>in pixels
    /// <param name="size_y"></param>in pixels
    /// <returns></returns>
    public static Vector4 TileCollide(Vector2 position, int size_x, int size_y)
    {
        Vector4 collided = Vector4.Zero;

        Vector2 world_position = position * 0.125f;

        int tilesX = (int)Math.Ceiling(size_x * 0.125f);
        int tilesY = (int)Math.Ceiling(size_y * 0.125f);


        float x = world_position.X + (tilesX * 0.5f);
        float y = world_position.Y + (tilesY * 0.5f); 

        if (TileCollidable(ToWorld(x + tilesX * 0.5f, y)))
            collided.X = 1;
             if (TileCollidable(ToWorld(x - tilesX * 0.5f, y)))
            collided.Y = 1;
             if (TileCollidable(ToWorld(x , y - tilesY * 0.5f)))
            collided.W = 1;
             if (TileCollidable(ToWorld(x, y + tilesY * 0.5f)))
            collided.Z = 1;

        //Console.WriteLine($"new collis)ion: {tilesX} and {tilesY}");
       /* for (int ty = 0; ty < tilesY; ty++)
        {
            for (int tx = 0; tx < tilesX; tx++)
            {
                //Console.WriteLine(world_position);
                
                int x = (int)Math.Round(world_position.X - tx  + (tilesX * 0.5f) + worldX);
                int y = (int)Math.Round(world_position.Y - ty + (tilesY * 0.5f) + worldY);   
              
                //Console.WriteLine($"{x}, {y}");
  
                if (Global.current_world.world[x, y].Collidable == true)
                {
                   // Global.current_world.SetTile(x , y, TileID.Tile_dirt);
                   if (collided.X != 0 )
                        collided.X = 1;
                    if (collided.X != 0 )
                        collided.X = 1;
                    if (collided.X != 0 )
                        collided.X = 1;
                    if (collided.X != 0 )
                        collided.X = 1;
                }
            }
            
        }

        //Console.WriteLine("Finished Collision Process.");
*/

        return collided;
    }
    private static bool TileCollidable(Vector2 pos) // SingleTileCollision
    {
        return Global.current_world.world[(int)pos.X, (int)pos.Y].Collidable;
    }
    private static Vector2 ToWorld(float _x, float _y)
    {
        
        int x = (int)Math.Round(_x + worldX);
        int y = (int)Math.Round(_y + worldY);  

        return new Vector2(x, y);
    }
}