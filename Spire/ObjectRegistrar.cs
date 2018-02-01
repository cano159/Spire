using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Spire
{
    public class ObjectRegistrar<T>
    {
        public IEnumerable<T> this[Mod mod] => RegisteredObjects[mod];
        public readonly ConcurrentDictionary<Mod, ConcurrentBag<T>> RegisteredObjects = new ConcurrentDictionary<Mod, ConcurrentBag<T>>();

        public Dictionary<Mod, ConcurrentBag<T>> FromActive()
        {
            return RegisteredObjects.Where(x => x.Key.IsActive)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public void RevokeAll(Mod parentMod)
        {
            RegisteredObjects[parentMod] = new ConcurrentBag<T>();
        }

        public bool HasRegisteredObjects()
        {
            return RegisteredObjects.Count > 0;
        }

        public void Add<TU>(Mod self, TU obj) where TU : T
        {
            if (!RegisteredObjects.ContainsKey(self))
                RegisteredObjects[self] = new ConcurrentBag<T>();

            if (!RegisteredObjects[self].Contains(obj))
                RegisteredObjects[self].Add(obj);
        }

        public void AddRange<TU>(Mod self, IEnumerable<TU> objs) where TU : T
        {
            foreach (TU obj in objs)
                Add(self, obj);
        }
    }
}