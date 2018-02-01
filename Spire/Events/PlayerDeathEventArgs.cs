using System;
using Microsoft.Xna.Framework;
using TowerFall;

namespace Spire.Events
{
    public class PlayerDeathEventArgs : EventArgs
    {
        public Player Player { get; }
        public PlayerCorpse Corpse { get; }
        public DeathCause Cause { get; }
        public Vector2 Position { get; }
        public int PlayerIndex { get; }
        public int KillerIndex { get; }

        public PlayerDeathEventArgs(Player player, PlayerCorpse corpse, int playerIndex, DeathCause cause,
            Vector2 position, int killerIndex)
        {
            Player = player;
            Corpse = corpse;
            Cause = cause;
            Position = position;
            PlayerIndex = playerIndex;
            KillerIndex = killerIndex;
        }
    }
}