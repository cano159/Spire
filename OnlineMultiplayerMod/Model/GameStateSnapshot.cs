using System.Collections.Generic;
using Monocle;
using MsgPack.Serialization;
using OnlineMultiplayerMod.Model.State;
using TowerFall;

namespace OnlineMultiplayerMod.Model
{
    public class GameStateSnapshot
    {
        public Scene CurrentScene { get; }
        public Level CurrentLevel { get; }

        private Engine Instance { get; }

        [MessagePackMember(0)] public SessionState Session;

        [MessagePackMember(1)] public IEnumerable<SyncableActor> SyncableActors;

        private GameStateSnapshot(Engine engine)
        {
            Instance = engine;

            CurrentScene = Engine.Instance.Scene;

            CurrentLevel = Instance.Scene as Level;

            if (CurrentLevel == null)
                return;

            Session = new SessionState(CurrentLevel.Session);
        }

        public static GameStateSnapshot TakeSnapshot()
        {
            return new GameStateSnapshot(Engine.Instance);
        }
    }
}