using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using trails.Script;
using TorraFramework.Core;

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
        AssetManager.Init(Content, GraphicsDevice, _spriteBatch);


        world = new World(32*32,32*32, AssetManager.LoadTexture("TileAtlas", "TileAtlas"));

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
        GraphicsDevice.Clear(new Color(225, 232, 255));
      
        world.DrawChunks(_spriteBatch);//_spriteBatch.Draw(TileAtlas, TileAtlas.Bounds, Color.Bisque);
       
        _spriteBatch.Begin();
        EntityManager.Draw(_spriteBatch);
        _spriteBatch.End();

        _spriteBatch.Begin();
        GuiManager.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
