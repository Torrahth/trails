using System;
using Microsoft.Xna.Framework.Input;

namespace TorraFramework.Core;

public static class KeyboardManager{

    public static KeyboardState last_state =  Keyboard.GetState();
    public static KeyboardState current_state =  Keyboard.GetState();

    public static bool KeyJustPressed(Keys key)
    {
        current_state =  Keyboard.GetState();
        bool pressed = last_state.IsKeyUp(key) && current_state.IsKeyDown(key);
        last_state = Keyboard.GetState();

        return pressed;
    }
    public static bool KeyJustReleased(Keys key)
    {
        current_state =  Keyboard.GetState();
        bool pressed =  last_state.IsKeyDown(key) && current_state.IsKeyUp(key);
        last_state = Keyboard.GetState();

        return pressed;
    }
    public static void UpdateState()
    {
        last_state = Keyboard.GetState();
    }
}