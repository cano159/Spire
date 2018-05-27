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
        public TowerFall.MainMenu MainMenu { get; }
        public MenuState State { get; }

        public MainMenuStateEventArgs(TowerFall.MainMenu menu, MenuState state)
        {
            MainMenu = menu;
            State = state;
        }
    }
}
