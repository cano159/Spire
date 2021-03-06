﻿using System;
using Microsoft.Xna.Framework;
using Monocle;
using TowerFall;
using static TowerFall.MainMenu;

namespace Spire.Events
{
    public class EventController
    {
        private static EventController _instance;
        public static EventController Instance => _instance ?? (_instance = new EventController());

        public event EventHandler<EventArgs> OnDrawBegin;
        public event EventHandler<EventArgs> OnGameInitializationStart;
        public event EventHandler<EventArgs> OnGameLoaded;
        public event EventHandler<EntityAddedEventArgs> OnEntityAddedToCurrentScene;
        public event EventHandler<EntityRemovedEventArgs> OnEntityRemovedFromCurrentScene;
        public event EventHandler<SessionStartEventArgs> OnSessionStart;
        public event EventHandler<PlayerSpawnEventArgs> OnPlayerSpawn;
        public event EventHandler<PlayerDeathEventArgs> OnPlayerDeath;
        public event EventHandler<RoundEventArgs> OnRoundStart;
        public event EventHandler<RoundEventArgs> OnRoundEnd;
        public event EventHandler<SceneEventArgs> OnSceneBegin;
        public event EventHandler<SceneEventArgs> OnSceneChange;
        public event EventHandler<SceneEventArgs> OnSceneEnd;
        public event EventHandler<GameUpdatedEventArgs> OnGameUpdate;
        public event EventHandler<MainMenuStateEventArgs> OnMainMenuStateChange;
        public event EventHandler<PlayerUpdateEventArgs> OnPlayerUpdate;

        public event EventHandler<ActorEventArgs> OnActorAdded;
        public event EventHandler<ActorEventArgs> OnActorRemoved;

        public event EventHandler<CommandsLogEventArgs> OnCommandsLogFunctionInvoke;
        public event EventHandler<CommandsTraceEventArgs> OnCommandTraceFunctionInvoke;


        public event EventHandler<EventArgs> OnScreenRender;

        internal void GameInitializationStart()
        {
            OnGameInitializationStart?.Invoke(this, new EventArgs());
        }

        internal void OnEntityAdded(Entity entity)
        {
            OnEntityAddedToCurrentScene?.Invoke(this, new EntityAddedEventArgs(entity));
        }

        internal void OnEntityRemoved(Entity entity)
        {
            OnEntityRemovedFromCurrentScene?.Invoke(this, new EntityRemovedEventArgs(entity));
        }

        internal void RoundBegin(RoundLogic logic)
        {
            OnRoundStart?.Invoke(this, new RoundEventArgs(logic));
        }

        internal void RoundEnd(RoundLogic logic)
        {
            OnRoundEnd?.Invoke(this, new RoundEventArgs(logic));
        }

        internal void PlayerSpawn(Player e)
        {
            OnPlayerSpawn?.Invoke(this, new PlayerSpawnEventArgs(e));
        }

        internal void PlayerRespawn(Player player, Vector2 position)
        {
            OnPlayerSpawn?.Invoke(this, new PlayerSpawnEventArgs(player));
        }

        internal void PlayerDeath(Player player, PlayerCorpse corpse, int playerIndex, DeathCause cause,
            Vector2 position, int killerIndex)
        {
            OnPlayerDeath?.Invoke(this,
                new PlayerDeathEventArgs(player, corpse, playerIndex, cause, position, killerIndex));
        }

        internal void SceneUpdate(Scene scene)
        {
            OnSceneChange?.Invoke(this, new SceneEventArgs(scene));
        }

        internal void SceneBegin(Scene scene)
        {
            OnSceneBegin?.Invoke(this, new SceneEventArgs(scene));
        }

        internal void SceneChange(Scene scene)
        {
            OnSceneChange?.Invoke(this, new SceneEventArgs(scene));
        }

        internal void SceneEnd(Scene scene)
        {
            OnSceneEnd?.Invoke(this, new SceneEventArgs(scene));
        }

        internal void SessionStart(Session session)
        {
            OnSessionStart?.Invoke(this, new SessionStartEventArgs(session.MatchSettings, session.TreasureSpawner));
        }

        internal void GameUpdate(GameTime time)
        {
            OnGameUpdate?.Invoke(this, new GameUpdatedEventArgs(time));
        }

        internal void DrawBegin()
        {
            OnDrawBegin?.Invoke(this, new EventArgs());
        }

        internal void Render()
        {
            OnScreenRender?.Invoke(this, new EventArgs());
        }

        internal void GameLoaded()
        {
            OnGameLoaded?.Invoke(this, EventArgs.Empty);
        }

        internal void MainMenuStateChanged(TowerFall.MainMenu mainMenu, MenuState state)
        {
            OnMainMenuStateChange?.Invoke(this, new MainMenuStateEventArgs(mainMenu, state));
        }

        internal void PlayerUpdate(Player player)
        {
            OnPlayerUpdate?.Invoke(this, new PlayerUpdateEventArgs(player));
        }

        internal void CommandLogFunction(string str)
        {
            OnCommandsLogFunctionInvoke?.Invoke(this, new CommandsLogEventArgs(str));
        }

        internal void CommandTraceFunction(object str)
        {
            OnCommandTraceFunctionInvoke?.Invoke(this, new CommandsTraceEventArgs(str));
        }
        internal void ActorAdded(Actor actor)
        {
            OnActorAdded?.Invoke(this, new ActorEventArgs(actor));
        }

        internal void ActorRemoved(Actor actor)
        {
            OnActorRemoved?.Invoke(this, new ActorEventArgs(actor));
        }
    }
}