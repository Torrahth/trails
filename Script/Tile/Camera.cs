using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace trails.Script;

public static class Camera
{
    static float zoom=1.0f;
    static Vector2 position;
    static Vector2 true_position;
    static Vector2 viewport ;
    static GameWindow window = null;
    static float camera_speed = 4;
    public static void Init(GameWindow _window)
    {
        window = _window;
    }

    public static void Update()
    {

        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.PageUp))
        {
            zoom = MathHelper.Clamp(zoom - 0.01f, 0.1f, 4.0f);
        }
        else if (keyboardState.IsKeyDown(Keys.PageDown))
        {
            zoom = MathHelper.Clamp(zoom + 0.01f, 1f, 4.0f);
        }


        int left = keyboardState.IsKeyDown(Keys.Left) ? 1 : 0 ;
        int right = keyboardState.IsKeyDown(Keys.Right) ? 1 : 0 ;

        int up = keyboardState.IsKeyDown(Keys.Up) ? 1 : 0 ;
        int down = keyboardState.IsKeyDown(Keys.Down) ? 1 : 0 ;

        int horizontal  = left - right;
        int vertical = up - down;

         viewport = window.ClientBounds.Size.ToVector2() *0.5f;

        true_position = position + new Vector2(-viewport.X, viewport.Y);

        if (keyboardState.IsKeyDown(Keys.LeftShift))
        {
            camera_speed = MathHelper.Clamp(camera_speed + 1.1f, 4.0f, 42.0f);
        }
        else
        {
             camera_speed = MathHelper.Clamp(camera_speed - 1.1f, 4.0f, 42.0f);
        }

       /* position.X -= horizontal * camera_speed;
        position.Y -= vertical * camera_speed;*/
   
    }
    public static Vector2 GetPosition()
    {
        return position;
    }
    public static void SetPosition(Vector2 new_pos)
    {
        position = new_pos;
    }
      public static Vector2 GetViewport()
    {
        return viewport;
    }
    public static float GetZoom()
    {
        return zoom;
    }
}