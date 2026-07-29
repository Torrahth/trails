using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TorraFramework.Core;

public class GameCore : Game
{
    public  GraphicsDeviceManager _graphics;
    public  SpriteBatch _spriteBatch;
    public  GraphicsAdapter graphicsAdapter;
    public  GraphicsProfile graphicsProfile;
    public  PresentationParameters presentationParameters;
    public  ContentManager contentManager;
  
    public GameCore(string Title, int width, int height)
    {
        Window.Title = Title;
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = width,
            PreferredBackBufferHeight = height
        };

        graphicsAdapter = GraphicsAdapter.DefaultAdapter;
        graphicsProfile = _graphics.GraphicsProfile;
        contentManager = Content;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
   
    public GraphicsAdapter RetrieveGraphicsAdapter()
    {
        return graphicsAdapter;
    }
    public GraphicsDevice GetGraphicsDevice()
    {
        return GraphicsDevice;
    }
    public ContentManager GetContentManager()
    {
        return contentManager;
    }
 
}