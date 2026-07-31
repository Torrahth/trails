using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TorraFramework.Core;

public static class Camera
{
    private static float zoom=1.0f;
    private static Vector2 position = Vector2.Zero;
    private static Vector2 true_position = Vector2.Zero;
    private static Vector2 viewport = Vector2.Zero;
    private static GameWindow window = null;
    private static float camera_speed = 4;

    public static void Init(GameWindow _window)
    {
        window = _window;
    }
    public static void Update()
    {
        KeyboardState keyboardState = Keyboard.GetState();
        viewport = Main.GetViewportSize() * 0.5f;

        if (keyboardState.IsKeyDown(Keys.OemMinus))
        {
            zoom = MathHelper.Clamp(zoom - 0.1f, 0.1f, 4.0f);
        }
        else if (keyboardState.IsKeyDown(Keys.OemPlus))
        {
            zoom = MathHelper.Clamp(zoom + 0.1f, 0.1f, 4.0f);
        }

        if (keyboardState.IsKeyDown(Keys.LeftShift))
        {
            camera_speed = MathHelper.Clamp(camera_speed + 1.1f, 2.0f, 42.0f);
        }
        else
        {
             camera_speed = MathHelper.Clamp(camera_speed - 1.1f, 2.0f, 42.0f);
        }


        //FreeMovement(keyboardState);

        true_position = position + new Vector2(-viewport.X, viewport.Y);
    }
    private static void FreeMovement(KeyboardState keyboardState)
    {
        int left = keyboardState.IsKeyDown(Keys.Left) ? 1 : 0 ;
        int right = keyboardState.IsKeyDown(Keys.Right) ? 1 : 0 ;

        int up = keyboardState.IsKeyDown(Keys.Up) ? 1 : 0 ;
        int down = keyboardState.IsKeyDown(Keys.Down) ? 1 : 0 ;

        int horizontal  = left - right;
        int vertical = up - down;

        position.X += horizontal * 4;
        position.Y += vertical * 4;
    }
    public static Vector2 GetPosition()
    {
        return position;
    }
    public static void SetPosition(Vector2 new_pos)
    {
        position = new_pos;
    }
    public static Vector2 GetHalfViewport()
    {
        return viewport;
    }
    public static Vector2 ConvertToCameraPosition(Vector2 pos)
    {
        // camera pos - object pos 
        return (new Vector2(  position.X- pos.X, position.Y - pos.Y )  * zoom) + viewport  ; 
    }
      public static Vector2 ConvertToInvertedCameraPosition(Vector2 pos)
    {
        // camera pos - object pos 
        return (new Vector2(   pos.X - position.X, pos.Y - position.Y  )  * zoom) + viewport  ; 
    }
     public static Vector2 NormalConvertToCameraPosition(Vector2 pos)
    {

        float newX = -1 * pos.X;
        float newY = 1 * pos.Y;
        return  (position - new Vector2(newX, newY)  / zoom)   ; 
    }
    public static float GetZoom()
    {
        return zoom;
    }
}