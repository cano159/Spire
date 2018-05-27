using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class SessionState
    {
        public int RoundIndex { get; }
        public RoundLogicState RoundLogic { get; }
        public int[] Scores { get; }
        public bool IsInOvertime { get; }
        public ArrowTypes RoundRandomArrowType { get; }
        public MatchSettingsState MatchSettings { get; }
        public bool WasInOvertime { get; }
        public DarkWorldSessionState DarkWorldState;

        public SessionState(Session session)
        {
            RoundIndex = session.RoundIndex;
            RoundLogic = new RoundLogicState(session.RoundLogic);
            IsInOvertime = session.IsInOvertime;
            Scores = session.Scores;
            RoundRandomArrowType = session.RoundRandomArrowType;
            MatchSettings = new MatchSettingsState(session.MatchSettings);
            WasInOvertime = session.WasInOvertime;

            if (session.DarkWorldState != null)
            {
                DarkWorldState = new DarkWorldSessionState(session.DarkWorldState);
            }
        }
    }
}