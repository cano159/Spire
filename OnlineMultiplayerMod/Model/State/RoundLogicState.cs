using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class RoundLogicState
    {
        public long Time { get; set; }
        public int[] Kills { get; set; }
        public int[] ScoreLost { get; set; }
        public int[] ScoreGained { get; set; }
        public bool RoundStarted { get; set; }
        public static bool Restarted { get; set; }
        public bool CanMiasma { get; set; }

        public int Players { get; set; }

        public RoundLogicState(RoundLogic logic)
        {
            Time = logic.Time;
            Kills = logic.Kills;
            ScoreLost = logic.ScoreLost;
            ScoreGained = logic.ScoreGained;
            RoundStarted = logic.RoundStarted;
            CanMiasma = logic.CanMiasma;
            Players = logic.Players;
            Restarted = RoundLogic.Restarted;
        }
    }
}