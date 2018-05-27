using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class LevelState
    {
        public int LivingPlayers { get; }
        public bool Paused { get; }
        public bool Ending { get; }
        public int LoadSeed { get; }
        public bool Frozen { get; }
        public bool CanSpawnTreasure { get; }
        public bool CanEnd { get; }

        public LevelState(Level input)
        {
            LivingPlayers = input.LivingPlayers;
            Paused = input.Paused;
            Ending = input.Ending;
            LoadSeed = input.LoadSeed;
            Frozen = input.Frozen;
            CanSpawnTreasure = input.CanSpawnTreasure;
            CanEnd = input.CanEnd;
        }
    }
}