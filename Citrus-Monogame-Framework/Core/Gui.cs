using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TorraFramework.Core;

public class Gui
{
    int X = 0;
    int Y = 0;
    int WIDTH = 0;
    int HEIGHT = 0;
    Texture2D texture;
    public Gui(int x=0, int y=0, int width=100, int height=100)
    {
        X = x;
        Y = y;
        WIDTH = width;
        HEIGHT = height;
        texture = AssetManager.GenerateTexture(WIDTH, HEIGHT, Color.Yellow);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, new Vector2(X, Y), new Rectangle(X, Y, WIDTH, HEIGHT), Color.White );
    }
}