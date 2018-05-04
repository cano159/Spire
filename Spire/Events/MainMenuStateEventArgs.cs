using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerFall;
using static TowerFall.MainMenu;

namespace Spire.Events
{
    public class MainMenuStateEventArgs : EventArgs
    {
        public MainMenu MainMenu { get; }
        public MenuState State { get; }

        public MainMenuStateEventArgs(MainMenu menu, MenuState state)
        {
            MainMenu = menu;
            State = state;
        }
    }
}
