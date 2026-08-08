using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TorraFramework.Core;

public class GuiButton : Gui
{
    //public event EventHandler<EventArgs> PressedDown;
    public override void Update()
    {
        if (Hover())
        {
            Main.MouseOverGui = true;

            color = Color.Gray;
            Pressed();
        }
        else
            color = Color.White;
    }
    public Rectangle Hitbox()
    { // (int)(texture.Width* size*0.125f) , (int)(texture.Height* size*0.125f
    int i =  (int)(texture.Width*0.125f* size);
    int j = (int)(texture.Height*0.125f* size);
        return new Rectangle(X+(int)(0), Y+(int)(0),(int)(i*0.5f), (int)(j*0.5f));
    }
    public bool Hover()
    {
        return Hitbox().Contains(Mouse.GetState().Position.ToVector2() + (rectangle.Size.ToVector2()));
    } 
    public bool Pressed()
    {
       // PressedDown.Invoke(this, EventArgs.Empty);
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
        OnPressed();
        return true;
        }
        return false;
        
    }
    public virtual void OnPressed()
    {
        
    }
}