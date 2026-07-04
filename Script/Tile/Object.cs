using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
namespace trails.Script;

public abstract class Object
{
    public Vector2 Position = new Vector2(0,0);
    public Vector2 Velocity = new Vector2(0,0);
    public float Rotation = 0;
    public Texture2D texture;
    public Color color = Color.White;
    public float scale = 1.0f;

   // public abstract void Init(Texture2D _texture);
    public virtual void Update()
    {
        
    }
    
    public virtual void Draw(SpriteBatch spriteBatch)
    {
                Vector2 position = (Position - Camera.GetPosition()) * Camera.GetZoom() +Camera.GetViewport();

                Vector2 texture_size = texture.Bounds.Size.ToVector2(); 
                Vector2 origin = new Vector2(texture_size.X, texture_size.Y)/ 2;

        spriteBatch.Draw(texture, position, texture.Bounds, color, Rotation, origin, scale* Camera.GetZoom(), SpriteEffects.None, 1.0f);
    }

}