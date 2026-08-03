using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using trails.Script;
using TorraFramework.Core;
using System.IO;
using System.Text.Json;

namespace trails;

public class Game1 : GameCore
{
    float frame_counter = 0.0f;
    // CONTENT
    World world;
    Player player;

    public Game1() : base("didy", 1000, 600)
    {
        Main.Window = Window;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;

        Camera.Init(Window);
    }
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        Setup();
    }
    public void Setup()
    {
        string file =  File.ReadAllText($"{AppContext.BaseDirectory}/game-config.json");
        Random rng = new Random();
        AssetManager.Init(Content, GraphicsDevice, _spriteBatch);
        
        JsonData d = JsonSerializer.Deserialize<JsonData>(file);

        world = new World(d.World_size_X *32,d.World_size_Y*32, AssetManager.LoadTexture("TileAtlas", "TileAtlas"), d.World_type); //rng.Next(0, 4)

        player = new Player(AssetManager.LoadTexture("Player", "PlayerFerret"));
        //Main.entityManager.CreateEntity(player);

        Global.current_world = world;
    }
    protected override void Update(GameTime gameTime)
    {
        if (!this.IsActive)
            return;
            
       
        Camera.Update();

        player.Update();
        world.Update();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Window.Title = "trails" + frame_counter;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        frame_counter = 1f / (float)gameTime.ElapsedGameTime.TotalSeconds;
        GraphicsDevice.Clear(Global.current_world.sky_color); //new Color(225, 232, 255));
         _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        world.DrawChunks(_spriteBatch);//_spriteBatch.Draw(TileAtlas, TileAtlas.Bounds, Color.Bisque);
        _spriteBatch.End();
       
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        EntityManager.Draw(_spriteBatch);
        _spriteBatch.End();

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        GuiManager.Draw(_spriteBatch);
        _spriteBatch.End();

        AssetManager.DrawQueue();

        base.Draw(gameTime);
    }
}
