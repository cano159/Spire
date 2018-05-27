using System;
using System.Collections.Concurrent;
using System.Threading;
using Harmony;
using Microsoft.Xna.Framework;
using Monocle;
using OnlineMultiplayerMod.Model;
using Spire;
using Spire.Events;
using TowerFall;

namespace OnlineMultiplayerMod
{
    public class OnlineMultiplayerMod : Mod
    {
        public static readonly ConcurrentDictionary<Actor, SyncableActor>
            SyncableActors = new ConcurrentDictionary<Actor, SyncableActor>();

        public override string ModName => "Online Multiplayer Mod";
        public override string ModAuthor => "ngrst183";
        public override string ModDescription => "Allows multiplayer and co-op games over the internet.";

        private readonly ThrottleCalculator _calculator = new ThrottleCalculator(30);

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private bool _isLoaded;

        public override void OnModEnabled()
        {
            EntityTypeWrappers.Instantiate();
            EventController.Instance.OnMainMenuStateChange += Instance_OnMainMenuStateChange;
        }

        public override void OnModDisabled()
        {
        }

        private void Instance_OnMainMenuStateChange(object sender, MainMenuStateEventArgs e)
        {
            if (e.State == MainMenu.MenuState.None || _isLoaded) return;

            _isLoaded = true;
        }

        public override void Update(GameTime time)
        {
            if (!(Engine.Instance.Scene is Level))
                return;

            foreach (Entity actor in Engine.Instance.Scene[GameTags.Actor])
                SyncableActors.TryAdd(actor as Actor, new SyncableActor(actor as Actor));
        }
    }
}