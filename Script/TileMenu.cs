using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TorraFramework.Core;

namespace trails.Script;

public class TileMenu : Gui
{
    
    //GuiButton[,] guis =  new GuiButton[4,4];
    public class TileButton : GuiButton
    {
        public Tile tile {get; set;}
        public override void OnPressed()
        {
            Global.players[0].tile_type = tile;
        }
    }
    public List<TileButton> guis = new List<TileButton>();

    public TileMenu(){
        WIDTH = 120;
        HEIGHT = 120;

        int _x = 1;
        int _y = 0;
        for (int y = 0; y < 4; y++){
            for (int x = 0; x < 4; x++){
                TileButton button = new TileButton();
                button.texture = AssetManager.LoadTexture("TileAtlas", "TileAtlas");
                button.X = (x * 20) + 40;
                button.Y = (y *20) + 40;
                button.size = 2.0f;
                button.WIDTH = button.Hitbox().X;
                button.HEIGHT = button.Hitbox().Y;
                button.tile = Tile.ConvertToTile(_x%4, _y%4);
                button.rectangle = new Microsoft.Xna.Framework.Rectangle(((_x%4)*8) *4, ((_y%4)*8) * 4,8, 8);
                guis.Add(button);//[x, y] = button;
                _x++;
            }
            _y++;
        }
        guis.EnsureCapacity(guis.Count);
    }
  
    public override void Update()
    {
        foreach (GuiButton button in guis)
        {
            button.Update();
        }
    }
     public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, new Vector2(X, Y), new Rectangle(X, Y, WIDTH, HEIGHT), color);
          foreach (GuiButton button in guis)
        {
            button.Draw(spriteBatch);
        }
    }
}