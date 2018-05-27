using System;
using System.Collections.Generic;
using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class TreasureSpawnerState
    {
        public float[] TreasureRates { get; private set; }
        public List<Pickups> Exclusions { get; private set; }

        public TreasureSpawnerState(TreasureSpawner spawner)
        {
            TreasureRates = spawner.TreasureRates;
            Exclusions = spawner.Exclusions;
        }
    }
}