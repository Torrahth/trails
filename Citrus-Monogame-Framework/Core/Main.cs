using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TorraFramework.Core;

public static class Main
{
    public static GameWindow Window;
    public static float Golden_ratio = 1.618033988749894f;
    public static Vector2 GetViewportSize()
    {
        return Window.ClientBounds.Size.ToVector2();
        //graphicsDevice.Viewport.Bounds.Size.ToVector2();
    }
    public static float DistanceFrom(Vector2 pos1, Vector2 pos2)
    {
        return (float)Math.Sqrt(Math.Pow(pos2.X - pos1.X , 2) + Math.Pow(pos2.Y - pos1.Y, 2));
    }
    public static float DistanceFrom(float pos1, float pos2)
    {
        return (float)Math.Abs(pos2 - pos1);//(float)Math.Sqrt(Math.Pow(pos2 - pos1 , 2) );
    }
}