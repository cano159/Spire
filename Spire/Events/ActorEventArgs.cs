using System;
using Monocle;
using TowerFall;

namespace Spire.Events
{
    public class ActorEventArgs : EventArgs
    {
        public Actor AddedActor { get; }

        public ActorEventArgs(Actor addedActor)
        {
            AddedActor = addedActor;
        }
    }
}