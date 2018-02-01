using System;
using Microsoft.Xna.Framework;

namespace Spire.Events
{
    public class GameUpdatedEventArgs : EventArgs
    {
        public GameTime Time { get; }

        public GameUpdatedEventArgs(GameTime time)
        {
            Time = time;
        }
    }
}