using System;
using Monocle;
using TowerFall;

namespace Spire.Events
{
    public class SessionStartEventArgs : EventArgs
    {
        public MatchSettings Settings { get; }
        public TreasureSpawner TreasureSpawner { get; }

        public SessionStartEventArgs(MatchSettings settings, TreasureSpawner treasureSpawner)
        {
            Settings = settings;
            TreasureSpawner = treasureSpawner;
        }
    }
}