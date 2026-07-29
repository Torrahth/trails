using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace TorraFramework.Core;

public static class AssetManager
{
    private static ContentManager contentManager { get; set;}
    private static GraphicsDevice graphicsDevice { get; set;}
    private static SpriteBatch spriteBatch { get; set;}

    public static List<string> textures_names = new List<string>();
    public static List<int> ids = new List<int>();

    public static List<Texture2D> textures_loaded = new List<Texture2D>();
    public static void Init(ContentManager cm, GraphicsDevice gd, SpriteBatch sb)
    {

        if (cm == null)
            new Exception("Content Manager Not Found");
        if (gd == null)
            new Exception("Graphics Device Not Found");
        if (sb == null)
            new Exception("SpriteBatch Not Found");
            
        contentManager = cm;
        graphicsDevice = gd;
        spriteBatch = sb;
    }
    public static Texture2D FindTexture(string AssetName)
    {
        return textures_loaded[textures_names.IndexOf(AssetName)];

    }
    public static Texture2D LoadTexture(string AssetName, string AssetPath)
    {
        Texture2D asset = contentManager.Load<Texture2D>(AssetPath);

        if (asset == null)
            new Exception("Asset Not Found!");
        
        if (textures_names.Contains(AssetName))
            FindTexture(AssetName);

        if (!textures_loaded.Contains(asset))
            textures_names.Add(AssetName);
            textures_loaded.Add(asset);
        Console.WriteLine(asset);
        return asset;
    }
    public static Texture2D LoadTexture(string AssetName,  Texture2D texture2D)
    {

        if (texture2D == null)
            new Exception("Asset Not Found!");
        
        if (!textures_loaded.Contains(texture2D))
            textures_names.Add(AssetName);
            textures_loaded.Add(texture2D);
        Console.WriteLine(texture2D);
        return texture2D;
    }
    public static void UnloadTexture(Texture2D texture)
    {
        if (texture == null || !textures_loaded.Contains(texture))
            return;

        textures_loaded.Remove(texture);
    }
    public static Texture2D GenerateTexture(int width, int height, Color color)
    {
        Texture2D texture = new Texture2D(graphicsDevice, width, height);

        Color[] data = new Color[width * height];


        for (int pixel = 0; pixel < data.Length; pixel++)
        {
            data.SetValue(color, pixel);

        }

        texture.SetData(data);
        return texture;

    }
    public static void QuickDraw(float x, float y, Texture2D texture)
    {
        spriteBatch.Begin();
        //Console.WriteLine($"yes bluds drawing: {texture}");
            Vector2 position = Camera.ConvertToCameraPosition(new Vector2(x, y));//(Position - Camera.GetPosition()) * Camera.GetZoom() + Main.GetViewportSize();

        Vector2 texture_size = texture.Bounds.Size.ToVector2(); 
        Vector2 origin = new Vector2(texture_size.X, texture_size.Y) * 0.5f;

        spriteBatch.Draw(texture, position, texture.Bounds, Color.White, 0, origin, 1.0f * Camera.GetZoom(), SpriteEffects.None, 0.0f);
        spriteBatch.End();
    }

}