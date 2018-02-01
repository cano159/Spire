using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Monocle;
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
