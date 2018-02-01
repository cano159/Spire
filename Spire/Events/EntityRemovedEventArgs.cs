using System;
using Monocle;

namespace Spire.Events
{
    public class EntityRemovedEventArgs : EventArgs
    {
        public Entity RemovedEntity { get; }

        public EntityRemovedEventArgs(Entity removedEntity)
        {
            RemovedEntity = removedEntity;
        }
    }
}