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

    public World(int sizeX, int sizeY, Texture2D new_texture, int World_type)
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

        new WorldGenTypes(this, World_type);
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
       
       float camera_zoom = Camera.GetZoom();

        foreach (Chunk chunk in chunks.get_chunks())
        {
            rng = new Random((int)chunk.GetCoords().Length());

            for (int x = 0; x < 32; ++x)
            {
                for (int y = 0; y < 32; y++)
                {
                    Vector2 tile = new Vector2(x, y)  * 8;
                    Vector2 position = tile+  ((new Vector2(32, 32)* chunk.GetCoords())*8)-new Vector2(8, 8) ;

                    Vector2 draw_pos = (position - Camera.GetPosition()) * camera_zoom ;
                    if ( Global.Intersects(draw_pos+ new Vector2(16, 16),(Main.GetViewportSize()*0.5f) + new Vector2(16, 16)))
                        continue;
                    
                    
                    Tile C_tile = chunk.chunk[x, y];
                    Vector2 texture_size = C_tile.texture_bounds.Size.ToVector2(); 
                    Vector2 origin = new Vector2(texture_size.X, texture_size.Y) * 0.5f;

                
                spriteBatch.Draw(texture, draw_pos+Camera.GetHalfViewport(), C_tile.texture_bounds, Color.White, 0, origin - new Vector2(4, 4), camera_zoom, rng.Next(0, 2) == 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 1.0f);
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
    public void GenerateSeaWeed(int x, int y, Tile tile_type, int size_x=1, float size_y=1, int width=1, int height=1, int chance_to_change=1)
    {
        int temp = 0;
        Random rng = new Random();

            for (int Y = 0; Y < size_y; Y++)
            {
                if (rng.Next(0, chance_to_change) == 0)
                    temp = Math.Clamp(temp + rng.Next(-1, 2), -size_x, size_x);
                int _x = x ;
                int _y = y - Y;// - (int)Math.Floor(size_y * 0.5f);
                if (!Is_in_world(_x, _y, 0))
                    return;

                if (width == 1)
                    SetTile(_x + temp, _y, tile_type);
                else
                    GenerateRectangle(_x + temp, _y, tile_type, width, height);
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
    public void GenerateFissure(int x, int y, Random rng, Tile tileid, int amount, int count=1)
    {
     
        for (int C = 0; C < count; C++)
        {
           int NX = x;
           int NY = y;
           for (int i = 0; i < amount; i++)
           {
                NX += rng.Next(0, 2) == 0 ? -1 : 1;
                NY += rng.Next(0, 2) == 0 ? -1 : 1;

                SetTile(NX, NY, tileid);
            }
        }
    }
    
    public void GenerateFissure(int x, int y, Random rng, Tile tileid, int amount, int size_X, int size_Y, bool fade, int count=1)
    {

        for (int C = 0; C < count; C++)
        {
            int NX = x;
            int NY = y;
            for (int i = 0; i < amount; i++)
            {
                NX += rng.Next(0, 2) == 0 ? -1 : 1;
                NY += rng.Next(0, 2) == 0 ? -1 : 1;
                GenerateRectangle(NX, NY, tileid, size_X, size_Y);// SetTile(x, y, tileid);
            }
       }
    }
    
}