using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class MatchTeamsState
    {
        public Allegiance Player1Team;
        public Allegiance Player2Team;
        public Allegiance Player3Team;
        public Allegiance Player4Team;

        public MatchTeamsState(MatchTeams teams)
        {
            Player1Team = teams.player1Team;
            Player2Team = teams.player2Team;
            Player3Team = teams.player3Team;
            Player4Team = teams.player4Team;

        }
    }
}
