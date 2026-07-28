using System;
using System.Linq;
using System.Xml.Serialization;
using trails.GameContent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TorraFramework.Core;

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
                SetTile(X, Y, TileID.Tile_air);
            } 
        }
        GenerateWorld();
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
    public Tile GetTile(int x, int y)
    {
        return world[x, y];
    }
    public void Update()
    {
         chunks.Update();
    }
    public void DrawChunks(SpriteBatch spriteBatch)
    {
       float camera_zoom = Camera.GetZoom();
          Vector2 texture_size = texture.Bounds.Size.ToVector2(); 
                Vector2 origin = new Vector2(texture_size.X, texture_size.Y)/ 2;
        foreach (Chunk chunk in chunks.get_chunks())
        {
            for (int x = 0; x < 32; ++x)
            {
            for (int y = 0; y < 32; y++)
            {
                //Random rng = new Random(chunk.chunk_ID + x * y);

                Tile C_tile = chunk.chunk[x, y];
                Vector2 position = new Vector2(x, y)  * 8+  new Vector2(32, 32)* chunk.GetCoords()*8+new Vector2(8, 8);

                spriteBatch.Draw(texture, (position - Camera.GetPosition()) * camera_zoom +Camera.GetHalfViewport(), C_tile.texture_bounds, Color.White, 0, origin, camera_zoom, SpriteEffects.FlipHorizontally, 1.0f);
            }
        }
        }

        //Camera.GetPosition() / 8 - 
        
    }
    public void DrawWorld(SpriteBatch spriteBatch)
    {
        for (int x = 0; x < _sizeX; ++x)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                Random rng = new Random();

                var C_tile = world[x, y];

                Vector2 position = new Vector2(x, y) * 8;

                Vector2 texture_size = texture.Bounds.Size.ToVector2(); 
                Vector2 origin = new Vector2(texture_size.X, texture_size.Y)/ 2;

                SpriteEffects effects = rng.Next(0, 2) == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                spriteBatch.Draw(texture, ((position - Camera.GetPosition()) )* Camera.GetZoom() +Camera.GetHalfViewport(), C_tile.texture_bounds, Color.White, 0, origin, Camera.GetZoom(), effects, 1.0f);

            }
        }
    }
    void PrepWorld()
    {
        
    }
    public void GenerateWorld()
    {
         int border = 2;
        for (int X = 0; X < _sizeX; ++X)
        {
            for (int Y = 0; Y < _sizeY; Y++)
            {
                Random rng = new Random();

                if ((X < border || X > _sizeX-1 - border) || (Y < border || Y > _sizeY-1 - border))
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
        GenerateRectangle((int)(_sizeX * 0.5f), (int)(_sizeY * 0.5f), TileID.Tile_crystal, 3 * (_sizeX * 0.05f), 3* (_sizeX * 0.05f));
        GenerateRectangle((int)(_sizeX * 0.5f), (int)(_sizeY * 0.5f), TileID.Tile_Water, 1 * (_sizeX * 0.05f), 1* (_sizeX * 0.05f));

    }
    void GenerateRectangle(int x, int y, Tile tile_type, float size_x, float size_y)
    {

        size_x = (int)Math.Floor(size_x);
        size_y = (int)Math.Floor(size_y);

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