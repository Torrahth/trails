using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TorraFramework.Core;

public class Gui
{
    public int X = 0;
    public int Y = 0;
    public int WIDTH = 0;
    public int HEIGHT = 0;
    public Rectangle rectangle = Rectangle.Empty;
    Texture2D texture;
    public Color color = Color.White; 
    public Gui(int x=0, int y=0, int width=100, int height=100)
    {
        X = x;
        Y = y;
        WIDTH = width;
        HEIGHT = height;
        rectangle = new Rectangle(x, y, width, height);
        texture = AssetManager.GenerateTexture(WIDTH, HEIGHT, Color.Yellow);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, new Vector2(X, Y), new Rectangle(X, Y, WIDTH, HEIGHT), color);
    }
}