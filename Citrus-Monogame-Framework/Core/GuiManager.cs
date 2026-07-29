using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace TorraFramework.Core;

public static class GuiManager
{// youre making it so you can retrive the gui from the same datatype.
    public static List<String> gui_string = new List<String>();
    public static List<Gui> guis = new List<Gui>();

    public static Gui CreateGui(string name, int x, int y, int width, int height)
    {
        Gui gui = new Gui(x, y, width, height);
        gui_string.Add(name);
        guis.Add(gui);
        return gui;
    }
    public static Gui GetGui(string name)
    {
        if (name == null)
        {
             Console.WriteLine("String entered into GetGui set null");
            return null;
        }
        else if (gui_string.Contains(name))
        {
            Console.WriteLine(name + " Not found within GUIS");
            return null;
        }

        int index = gui_string.IndexOf(name);
        Console.WriteLine("Successful Retrieval of:", name);
        return guis[index];
    }
    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (Gui gui in guis)
        {
            gui.Draw(spriteBatch);
        }
    }

}