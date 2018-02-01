using System;
using TowerFall;

namespace Spire.Events
{
    public class RoundEventArgs : EventArgs
    {
        public RoundLogic RoundLogic { get; }

        public RoundEventArgs(RoundLogic logic)
        {
            RoundLogic = logic;
        }
    }
}