using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace trails.Script;

public static class Global
{
    public static World current_world;
    public static List<Player> players = new List<Player>();
    public static bool Intersects(Vector2 pos, Vector2 collider)
    {
        if (pos.Y > collider.Y)
        {
            return true;
        }
        if (pos.Y < -collider.Y)
        {
            return true;
        }//why dont i use fucking rectangles??? hello?

        if (pos.X > collider.X)
        {
            return true;
        }
        if (pos.X < -collider.X)
        {
            return true;
        }
        return false;
    }
    public static bool Intersects(Vector2 pos, Rectangle collider)
    {
        if (pos.X > collider.Left && pos.X < collider.Right && pos.Y > collider.Top && pos.Y < collider.Bottom)
        {
            return true;
        }
        return false;
    }
}