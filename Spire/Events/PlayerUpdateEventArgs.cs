using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerFall;

namespace Spire.Events
{
    public class PlayerUpdateEventArgs : EventArgs
    {
        public Player Player { get; }

        public PlayerUpdateEventArgs(Player player)
        {
            Player = player;
        }

    }
}
