using System;
using Microsoft.Xna.Framework;
using TowerFall;

namespace Spire.Events
{
    public class PlayerSpawnEventArgs : EventArgs
    {
        public Player Player { get; }

        public PlayerSpawnEventArgs(Player player)
        {
            Player = player;
        }
    }
}