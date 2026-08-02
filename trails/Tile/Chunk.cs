using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace trails.Script;

public class Chunk
{
    Vector2 chunk_pos;
    public int chunk_ID = 0;
    public int chunk_size = 32;
     public Tile[,] chunk = new Tile[32, 32];
     public List<Object> objects = new List<Object>();

    public void Init(int X, int Y)
    {
        chunk_ID = new Random().Next(0, 100);
        chunk_pos = new Vector2(X, Y);
        //uuuConsole.WriteLine(chunk_pos);
        for (int x = 0; x < chunk_size; ++x)
        {
            for (int y = 0;  y< chunk_size; ++y)
            {
                int cx = x+(chunk_size*X) + Global.current_world._sizeX/2;
                int cy =  y+ (chunk_size*Y)+ Global.current_world._sizeY/2;
                chunk[x,y] = Global.current_world.world[cx, cy];
            }
        }
    }
    public void Update()
    {
         for (int x = 0; x < chunk_size; ++x)
        {
            for (int y = 0;  y< chunk_size; ++y)
            {
                int cx = x+(chunk_size* (int)chunk_pos.X) + Global.current_world._sizeX/2;
                int cy =  y+ (chunk_size*(int)chunk_pos.Y) + Global.current_world._sizeY/2;
                chunk[x,y] = Global.current_world.world[cx, cy];
            }
        }
    }

    public Vector2 GetCoords()
    {
        return chunk_pos;
    }
    
}