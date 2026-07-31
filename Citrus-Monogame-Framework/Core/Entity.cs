using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace TorraFramework.Core;

public class Entity
{
    public Vector2 OldPosition = Vector2.Zero;
    public Vector2 Position = Vector2.Zero;
    public Vector2 Velocity = Vector2.Zero;
    public Vector4 Collisions = Vector4.Zero;
    public float Rotation = 0;
    public Texture2D texture;
    public Color color = Color.White;
    public float scale = 1.0f;
    public int DrawMode = 0;
    
    public Rectangle draw_rectangle = Rectangle.Empty;

   // public abstract void Init(Texture2D _texture);
    public Entity()
    {
        if (texture == null)
            texture = AssetManager.GenerateTexture(1, 1, Color.AntiqueWhite);
        EntityManager.CreateEntity(this);
    }
    public virtual void Update()
    {
        Position += Velocity;
    }
    
    
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (draw_rectangle.IsEmpty)
        {
            draw_rectangle = texture.Bounds;
        }
        switch (DrawMode)
        {
            case 0:
                {
                    StandardDraw(spriteBatch);
                }
                break;
            case 1:
                {
                    WithoutCameraDraw(spriteBatch);
                }
                break;
            case 2:
                {
                    ExactDraw(spriteBatch);
                }
                break;
        }
    
    }
    private void StandardDraw(SpriteBatch spriteBatch)
    {
        Vector2 position = Camera.ConvertToInvertedCameraPosition(Position);//(Position - Camera.GetPosition()) * Camera.GetZoom() + Main.GetViewportSize();

        Vector2 texture_size = draw_rectangle.Size.ToVector2(); 
        Vector2 origin = new Vector2(texture_size.X, texture_size.Y) * 0.5f;

        spriteBatch.Draw(texture, position,draw_rectangle, color, Rotation, origin, scale * Camera.GetZoom(), SpriteEffects.None, 1.0f);
    }
    private void WithoutCameraDraw(SpriteBatch spriteBatch)
    {
         Vector2 position = (-Position ) * Camera.GetZoom() + Main.GetViewportSize();

        Vector2 texture_size = draw_rectangle.Size.ToVector2(); 
        Vector2 origin = new Vector2(texture_size.X, texture_size.Y) * 0.5f;

        spriteBatch.Draw(texture, position, draw_rectangle, color, Rotation, origin, scale * Camera.GetZoom(), SpriteEffects.None, 1.0f);
    }
    private void ExactDraw(SpriteBatch spriteBatch)
    {
        Vector2 position = Position;

        Vector2 texture_size = draw_rectangle.Size.ToVector2(); 
        Vector2 origin = new Vector2(texture_size.X, texture_size.Y) * 0.5f;

        spriteBatch.Draw(texture, position, draw_rectangle, color, Rotation, origin, scale * Camera.GetZoom(), SpriteEffects.None, 1.0f);
    }
    public virtual void Draw2(SpriteBatch spriteBatch)
    {
                Vector2 position = (Position - Camera.GetPosition()) * Camera.GetZoom() +Camera.GetHalfViewport();

                Vector2 texture_size = draw_rectangle.Size.ToVector2(); 
                Vector2 origin = new Vector2(texture_size.X, texture_size.Y) * 0.5f;

        spriteBatch.Draw(texture, position, draw_rectangle, color, Rotation, origin, scale* Camera.GetZoom(), SpriteEffects.None, 1.0f);
    }
 

}