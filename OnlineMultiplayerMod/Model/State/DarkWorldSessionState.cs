using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMultiplayerMod.Model.State
{
    public class DarkWorldSessionState
    {
        public int ExtraLives;
        public int[] EnemyKills;
        public int[] BestCombos;
        public int[] Revives;
        public int[] Deaths;
        public int[] BossHits;
        public int[] OldBossHits;
        public long Time;
        public int LevelTries;

        public DarkWorldSessionState(TowerFall.DarkWorldSessionState state)
        {
            ExtraLives = state.ExtraLives;
            EnemyKills = state.EnemyKills;
            BestCombos = state.BestCombos;
            Revives = state.Revives;
            Deaths = state.Deaths;
            BossHits = state.BossHits;
            OldBossHits = state.OldBossHits;
            Time = state.Time;
            LevelTries = state.LevelTries;
        }
    }
}
