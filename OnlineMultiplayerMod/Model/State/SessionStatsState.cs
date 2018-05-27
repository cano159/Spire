using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class SessionStatsState
    {
        public int[] ArcherDeaths;
        public int[] ArcherKills;
        public int[] ArcherPlays;
        public int[] ArcherSelfKills;
        public int[] ArcherWins;
        public int MatchesPlayed;
        public int RoundsPlayed;
        public long TimePlayed;
        public int TotalVersusKills;

        public SessionStatsState()
        {
            ArcherDeaths = SessionStats.ArcherDeaths;
            ArcherKills = SessionStats.ArcherKills;
            ArcherPlays = SessionStats.ArcherPlays;
            ArcherSelfKills = SessionStats.ArcherSelfKills;
            ArcherWins = SessionStats.ArcherWins;
            MatchesPlayed = SessionStats.MatchesPlayed;
            RoundsPlayed = SessionStats.RoundsPlayed;
            TimePlayed = SessionStats.TimePlayed;
            TotalVersusKills = SessionStats.TotalVersusKills;
        }
    }
}