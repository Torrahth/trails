using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TorraFramework.Core;

namespace trails.Script;

public abstract class Object
{
    public Vector2 Position = Vector2.Zero;
    public Vector2 OldPosition = Vector2.Zero;
    public Vector2 Velocity = Vector2.Zero;
    public Vector4 Collisions = Vector4.Zero;
    public float Rotation = 0;
    public Texture2D texture;
    public Color color = Color.White;
    public float scale = 1.0f;

   // public abstract void Init(Texture2D _texture);
    public virtual void Update()
    {
        Position += Velocity;
    }
    
    public virtual void Draw(SpriteBatch spriteBatch)
    {
                Vector2 position = (Position - Camera.GetPosition()) * Camera.GetZoom() +Camera.GetHalfViewport();

                Vector2 texture_size = texture.Bounds.Size.ToVector2(); 
                Vector2 origin = new Vector2(texture_size.X, texture_size.Y)/ 2;

        spriteBatch.Draw(texture, position, texture.Bounds, color, Rotation, origin, scale* Camera.GetZoom(), SpriteEffects.None, 1.0f);
    }

}