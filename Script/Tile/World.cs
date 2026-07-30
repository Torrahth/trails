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
    public Color sky_color = Color.White;
    public Random rng = new Random();

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

        new WorldGenTypes(this, 2);
        world.Initialize();
    }
    public void SetTile(int x, int y, Tile tile_type)
    {
        if (!Is_in_world(x, y,0))
        {
            return;
        }
        world[x, y] =  tile_type;
    }
    public Tile GetTile(int x, int y)
    {
        return world[x + (int)(_sizeX * 0.5f), y + (int)(_sizeY * 0.5f)];
    }
    public void Update()
    {
         chunks.Update();
    }
    public void DrawChunks(SpriteBatch spriteBatch)
    {
       
        spriteBatch.Begin(samplerState: SamplerState.PointClamp );
       float camera_zoom = Camera.GetZoom();
        Vector2 texture_size = texture.Bounds.Size.ToVector2(); 
        Vector2 origin = new Vector2(texture_size.X, texture_size.Y) * 0.5f;
        foreach (Chunk chunk in chunks.get_chunks())
        {
            rng = new Random((int)chunk.GetCoords().Length());

            for (int x = 0; x < 32; ++x)
            {
                for (int y = 0; y < 32; y++)
                {

                Tile C_tile = chunk.chunk[x, y];
                Vector2 position = new Vector2(x, y)  * 8+  new Vector2(32, 32)* chunk.GetCoords()*8+new Vector2(8, 8);

                spriteBatch.Draw(texture, (position - Camera.GetPosition()) * camera_zoom +Camera.GetHalfViewport(), C_tile.texture_bounds, Color.White, 0, origin, camera_zoom, rng.Next(0, 2) == 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 1.0f);
                }
            }
        }
        spriteBatch.End();
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
   
    public void GenerateWorldBorder(Tile tile_type, int width)
    {
        for (int x = 0; x < _sizeX; ++x)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                if ((x < width || x > _sizeX-1 - width) || (y < width || y > _sizeY-1 - width))
                {
                    SetTile(x, y, tile_type);
                }
            }
        }
    }
    public void GenerateRectangle(int x, int y, Tile tile_type, float size_x, float size_y)
    {
      

        size_x = (int)Math.Floor(size_x);
        size_y = (int)Math.Floor(size_y);

        for (int X = 0; X < size_x; X++)
        {
            for (int Y = 0; Y < size_y; Y++)
            {
                int _x = x - X + (int)Math.Floor(size_x * 0.5f);
                int _y = y - Y + (int)Math.Floor(size_y * 0.5f);
                if (!Is_in_world(_x, _y, 0))
                    return;

                SetTile(_x, _y, tile_type);
            }
        }
    }
    public void GenerateOrb(int x, int y, Tile tile_type, int width)
    {
        int widthhalf = (int)Math.Ceiling(width *0.5f);
        for (int Y= 0; Y < width+1; Y++)
        {
        for (int X= 0; X < width+1; X++)
        {
            if (Math.Floor(Main.DistanceFrom(new Vector2(x +X - widthhalf, y+ Y - widthhalf), new Vector2(x, y))) < widthhalf)
                SetTile(x+(int)( X - widthhalf), y+ (int)( Y - widthhalf), tile_type);
           // else
        //        SetTile(x+(int)( X - widthhalf),  y+(int)( Y - widthhalf), TileID.Tile_Water);
        }
        }
       // SetTile(x, y, TileID.Fractal);

    }
    public void GenerateSeaWeed(int x, int y, Tile tile_type, float size_x, float size_y, int width)
    {
        int temp = 0;
        Random rng = new Random();

        size_x = (int)Math.Floor(size_x);
        size_y = (int)Math.Floor(size_y);

        for (int X = 0; X < size_x; X++)
        {
            for (int Y = 0; Y < size_y; Y++)
            {
                temp = Math.Clamp(temp + rng.Next(-1, 2), -width, width);
                int _x = x - X + (int)Math.Floor(size_x * 0.5f);
                int _y = y - Y + (int)Math.Floor(size_y * 0.5f);
                if (!Is_in_world(_x, _y, 0))
                    return;

                SetTile(_x + temp, _y, tile_type);
            }
        }
    }
    /// <summary>
    /// if is in world, border is for if is inside the world + border width
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="border"></param>
    /// <returns></returns>
    public bool Is_in_world(int x, int y, int border)
    {
        if (x >= border && x < _sizeX - border && y >= border && y < _sizeY - border)
        {
            return true;
        }
        return false;
    }
    public void GenerateFissure(int x, int y, Random rng, Tile tileid, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            x += rng.Next(0, 2) == 0 ? -1 : 1;
            y += rng.Next(0, 2) == 0 ? -1 : 1;

             SetTile(x, y, tileid);
        }
       
    }
}