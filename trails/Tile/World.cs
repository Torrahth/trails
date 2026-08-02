using System;
using System.Linq;
using System.Xml.Serialization;
using trails.GameContent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace trails.Script;

public class World
{
    public int _sizeX = 0;
    public int _sizeY = 0;
    public Tile[,] world = new Tile[0, 0];
    public Texture2D texture;
    public ChunkingSystem chunks;


    public World(int sizeX, int sizeY, Texture2D new_texture)
    {
        chunks = new ChunkingSystem(this);
        _sizeX = sizeX;
        _sizeY = sizeY;
        world = new Tile[_sizeX, _sizeY];

        texture = new_texture;

        for (int X = 0; X < _sizeX; ++X)
        {
            for (int Y = 0; Y < _sizeY; Y++)
            {
                SetTile(X, Y, TileID.Tile_brick);
            } 
        }
        Console.WriteLine("Begin Generation Process");

        GenerateWorld();
        Console.WriteLine("Finished Generation Steps");

        world.Initialize();
        
    }
    public void SetTile(int x, int y, Tile tile_type)
    {
        if (!Is_in_world(x, y))
        {
            return;
        }
          world[x, y] =  tile_type;
    }
    public void Update()
    {
         chunks.Update();
    }
    public void DrawChunks(SpriteBatch spriteBatch)
    {
        float camera_zoom = Camera.GetZoom();
        Vector2 texture_size = texture.Bounds.Size.ToVector2(); 
        Vector2 origin = new Vector2(texture_size.X, texture_size.Y)* 0.5f;

        foreach (Chunk chunk in chunks.get_chunks())
        {
            for (int x = 0; x < 32; ++x)
            {
            for (int y = 0; y < 32; y++)
            {
                //Random rng = new Random(chunk.chunk_ID + x * y);

                Tile C_tile = chunk.chunk[x, y];
                Vector2 position = new Vector2(x, y)  * 8+  new Vector2(32, 32)* chunk.GetCoords()*8+new Vector2(8, 8);

                spriteBatch.Draw(texture, (position - Camera.GetPosition()) * camera_zoom +Camera.GetViewport(), C_tile.texture_bounds, Color.White, 0, origin, camera_zoom, SpriteEffects.FlipHorizontally, 1.0f);
            }
        }
        }

        //Camera.GetPosition() / 8 - 
        
    }
    void PrepWorld()
    {
        
    }
    public void GenerateWorld()
    {
        Random rng = new Random();

         int border = 2;
        for (int X = 0; X < _sizeX; ++X)
        {
            for (int Y = 0; Y < _sizeY; Y++)
            {
                

                if (X < border || X > _sizeX-1 - border || Y < border || Y > _sizeY-1 - border)
                {
                    SetTile(X, Y, TileID.Tile_brick);
                }
                else if (Y < _sizeY / 3)
                {
                      if (rng.Next(0, 2) == 0)
                      SetTile(X, Y, TileID.Tile_dirt);
                    else
                        SetTile(X, Y, TileID.Tile_air);
                }
                
            } 
        }

        int world_origin_x  = (int)(_sizeX * 0.5f);
        int world_origin_y  = (int)(_sizeX * 0.5f);

        GenerateRectangle(world_origin_x, world_origin_y, TileID.Tile_air, 6 * (int)(_sizeX * 0.008),6 * (int)(_sizeX * 0.008));
    }
    void GenerateRectangle(int x, int y, Tile tile_type, int size_x, int size_y)
    {
        for (int X = 0; X < size_x; X++)
        {
            for (int Y = 0; Y < size_y; Y++)
            {
                int _x = x + X - (int)Math.Floor(size_x * 0.5f);
                int _y = y + Y - (int)Math.Floor(size_y * 0.5f);

                SetTile(_x, _y, tile_type);
            }
        }
    }
    public bool Is_in_world(int x, int y)
    {
        if (x >= 0 && x < _sizeX && y >= 0 && y < _sizeY)
        {
            return true;
        }
        return false;
    }
    
}