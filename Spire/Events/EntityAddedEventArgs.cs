using System;
using Monocle;

namespace Spire.Events
{
    public class EntityAddedEventArgs : EventArgs
    {
        public Entity AddedEntity { get; }

        public EntityAddedEventArgs(Entity addedEntity)
        {
            AddedEntity = addedEntity;
        }
    }
}