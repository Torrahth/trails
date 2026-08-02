using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TorraFramework.Core;
using System.IO;
using System.Text.Json;

namespace trails.Script;

public class ChunkingSystem
{
    List<Chunk> _active_chunks = new List<Chunk>(); 
    List<Vector2> _chunk_coords = new List<Vector2>(); // fuck you trora 

    List<Chunk> _deleting_chunks = new List<Chunk>();

    Chunk chunk;

    Vector2 TargetPosition = Camera.GetPosition()-Camera.GetHalfViewport();
    int chunk_rad_X = 12;
     int chunk_rad_Y = 10;
    public ChunkingSystem(World world)
    {
          string file =  File.ReadAllText($"{AppContext.BaseDirectory}/game-config.json");
        
        JsonData d = JsonSerializer.Deserialize<JsonData>(file);

        d.Chunk_radius = Math.Clamp(d.Chunk_radius, 1, 30);

        chunk_rad_X = d.Chunk_radius;
        chunk_rad_Y = d.Chunk_radius- 2;

         chunk = new Chunk();
        // Console.WriteLine("gulpss");

    }
    public void Update()
    {
        //int camera_zoom =  (4 - ((int)Camera.GetZoom())) * 2;

        var pow = 32;

        TargetPosition = (Camera.GetPosition()   - new Vector2(Global.current_world._sizeX/32, Global.current_world._sizeY/32))/8 ;
        Vector2 TargetChunk = new Vector2((int)Math.Ceiling(TargetPosition.X /pow ),(int)Math.Ceiling(TargetPosition.Y / pow)) ;
        //Console.WriteLine(TargetChunk );

        for (int x = - chunk_rad_X/2; x < chunk_rad_X/2; ++x)
        {
            for (int y = - chunk_rad_Y/2; y <  chunk_rad_Y/2; y++)
            {
               // Console.WriteLine("self centered: " + new Vector2(x, y));

               load_chunk((int)(x + (int)TargetChunk.X), (int)(y + (int)TargetChunk.Y));
            }
        }
        foreach (Chunk chunk in _active_chunks)
        {
            if (   Math.Abs(TargetChunk.X-chunk.GetCoords().X ) > chunk_rad_X/2)
            {
                _deleting_chunks.Add(chunk);
            }
              if (Math.Abs(TargetChunk.Y- chunk.GetCoords().Y ) > chunk_rad_Y/2)
            {
                _deleting_chunks.Add(chunk);
            }
        }
        foreach (Chunk chunk in _deleting_chunks)
        {
          //  Console.WriteLine("ye");
            _active_chunks.Remove(chunk);
            _chunk_coords.Remove(chunk.GetCoords());

        }
        _deleting_chunks.Clear();

    }
    public List<Chunk> get_chunks()
    {
        return _active_chunks;
    }
    void load_chunk(int x, int y)
    {
        if (!checkin(x, y))
             return;
        
        if (_chunk_coords.Contains(new Vector2(x, y)))
        {
            _active_chunks[_chunk_coords.IndexOf(new Vector2(x, y))].Update();
            return;
        }
            
      
    
        Chunk chunk = new Chunk();
        chunk.Init(x, y);

        _active_chunks.Add(chunk);
         _chunk_coords.Add(new Vector2(x, y));

        //Console.WriteLine(chunk.GetCoords());
    }
    bool checkin(int x, int y)
    {
        int cal = 64;
        // if (x >= 0 && x < Global.current_world._sizeX/32 && y >= 0 && y < Global.current_world._sizeY/32) //      
          if (x >= -Global.current_world._sizeX/cal && x < Global.current_world._sizeX/cal && y >= -Global.current_world._sizeY/cal && y < Global.current_world._sizeY/cal)
        {
                   // Console.WriteLine("Success at: " + new Vector2(x, y));

            return true;
        }
                       //     Console.WriteLine("Failure at at: " + new Vector2(x, y));

        return false;
        
    }

}