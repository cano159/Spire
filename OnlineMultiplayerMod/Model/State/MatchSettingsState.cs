using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class MatchSettingsState
    {
        public bool QuestHardcoreMode;
        public float[] TreasureRates;
        public MatchTeamsState Teams;
        public MatchSettings.MatchLengths MatchLength;
        public Modes Mode;
        public int RandomLevelSeed;
        public bool RandomVersusTower;
        public DarkWorldDifficulties DarkWorldDifficulty;
        public LevelSystemState LevelSystem;

        public MatchSettingsState(MatchSettings settings)
        {
            QuestHardcoreMode = settings.QuestHardcoreMode;
            TreasureRates = settings.TreasureRates;
            Teams = new MatchTeamsState(settings.Teams);
            MatchLength = settings.MatchLength;
            RandomLevelSeed = settings.RandomLevelSeed;
            Mode = settings.Mode;
            RandomVersusTower = settings.RandomVersusTower;
            DarkWorldDifficulty = settings.DarkWorldDifficulty;
            LevelSystem = new LevelSystemState(settings.LevelSystem);
        }
    }
}
