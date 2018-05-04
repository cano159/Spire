using Microsoft.Xna.Framework;
using Monocle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TowerFall;

namespace Spire.ModMenu
{
    public class ModsMenu : Scene
    {
        public IEnumerable<OptionsButton> Buttons;

        public static MethodInfo InitOptionsMethodInfo =
            typeof(MainMenu).GetMethod("InitOptions", BindingFlags.NonPublic | BindingFlags.Instance);

        public ModsMenu(MainMenu menu, IEnumerable<OptionsButton> modButtons)
        {
            OptionsButton[] buttonsArray = modButtons.ToArray();

            for (var i = 0; i < buttonsArray.Length; i++)
            {
                OptionsButton optionsButton = buttonsArray[i];
                optionsButton.TweenTo = new Vector2(200f, 45 + i * 12);
                optionsButton.Position = optionsButton.TweenFrom =
                    new Vector2(i % 2 == 0 ? -160 : 480, 45 + i * 12);
                if (i > 0)
                    optionsButton.UpItem = buttonsArray[i - 1];
                if (i < buttonsArray.Length - 1)
                    optionsButton.DownItem = buttonsArray[i + 1];
            }

            Buttons = buttonsArray.ToList();

            foreach (var button in Buttons)
            {
                menu.Add(button);
            }
        }
    }
}
