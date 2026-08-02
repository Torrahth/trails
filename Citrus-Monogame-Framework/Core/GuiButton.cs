using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using trails.Script;

namespace TorraFramework.Core;

public class GuiButton : Gui
{

    public GuiButton(int x=0, int y=0, int width=100, int height=100) : base(x, y, width, height)
    {

    }
    public void Update()
    {
        if (Hover())
            color = Color.Gray;
        else
            color = Color.White;
    }
    public bool Hover()
    {
        return Global.Intersects(Mouse.GetState().Position.ToVector2(), rectangle);
    }
    public bool Pressed(Vector2 pos)
    {
        return Hover() && Mouse.GetState().LeftButton == ButtonState.Pressed;
    }
}