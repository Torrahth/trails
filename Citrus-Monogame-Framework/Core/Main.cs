using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TorraFramework.Core;

public static class Main
{
    public static GameWindow Window;
    public static Vector2 GetViewportSize()
    {
        return Window.ClientBounds.Size.ToVector2();
        //graphicsDevice.Viewport.Bounds.Size.ToVector2();
    }
}