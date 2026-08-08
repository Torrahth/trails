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
    public Texture2D texture;
    public Color color = Color.White; 
    public float size = 1.0f;
    public Gui(int x=0, int y=0, int width=100, int height=100, Color _color=default, Rectangle _rectangle=default, Texture2D _texture=null)
    {
        X = x;
        Y = y;
        WIDTH = width;
        HEIGHT = height;
        color=_color;
        if (_rectangle == default || _rectangle == Rectangle.Empty)
            rectangle = new Rectangle(x, y, width, height);
            
        else
            rectangle = _rectangle;
        if (_texture == null)
            texture = AssetManager.GenerateTexture(WIDTH, HEIGHT, color);
        else
            texture = _texture;
    }
    public virtual void Update(){

    }
   
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, new Vector2(X, Y), rectangle, color, 0, rectangle.Size.ToVector2() * 0.5f, size, SpriteEffects.None, 1 );
    }
}