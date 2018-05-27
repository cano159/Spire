using System;
using System.Collections.Generic;
using Monocle;
using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class TreasureChestState
    {
        public readonly TreasureChest.AppearModes Mode;
        public readonly List<Pickups> Pickups;
        public readonly DeathSkull.Types Type;
        public TreasureChest.States State { get; private set; }
    }
}