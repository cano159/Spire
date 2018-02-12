using System;
using TowerFall;

namespace Spire.Events
{
    public class TfGameEventArgs : EventArgs
    {
        public TFGame Game { get; }

        public TfGameEventArgs(TFGame game)
        {
            Game = game;
        }
    }
}