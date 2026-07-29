using System;
using Microsoft.Xna.Framework.Input;

namespace TorraFramework.Core;

public static class KeyboardManager{

    public static KeyboardState last_state =  Keyboard.GetState();
    public static KeyboardState current_state =  Keyboard.GetState();

    public static bool KeyJustPressed(Keys key)
    {
        current_state =  Keyboard.GetState();
      
        return last_state.IsKeyUp(key) && current_state.IsKeyDown(key);
    }
    public static bool KeyJustReleased(Keys key)
    {
        current_state =  Keyboard.GetState();
      
        return last_state.IsKeyDown(key) && current_state.IsKeyUp(key);
    }
    public static void UpdateState()
    {
        last_state = Keyboard.GetState();
    }
}