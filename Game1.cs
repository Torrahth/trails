using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using trails.Script;

namespace trails;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    float frame_counter = 0.0f;
    // TEXTURE
    Texture2D TileAtlas;
    Texture2D Player_asset;
    // CONTENT
    World world;
    Player player;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
       
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Camera.Init(Window);
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        
        TileAtlas = Content.Load<Texture2D>("TileAtlas");
        Player_asset = Content.Load<Texture2D>("PlayerFerret");

        Setup();
    }
    public void Setup()
    {
        world = new World(62*32,62*32, TileAtlas);
        player = new Player(Player_asset);
        Global.current_world = world;
    }
    protected override void Update(GameTime gameTime)
    {
       
        player.Update();
        world.Update();
        Camera.Update();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();


         Window.Title = "trails" + frame_counter;
        // TODO: Add your update logic here

        base.Update(gameTime);
       
    }

    protected override void Draw(GameTime gameTime)
    {
          frame_counter = 1f / (float)gameTime.ElapsedGameTime.TotalSeconds;
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        world.DrawChunks(_spriteBatch);//_spriteBatch.Draw(TileAtlas, TileAtlas.Bounds, Color.Bisque);
        player.Draw(_spriteBatch);
        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
