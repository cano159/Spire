using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Monocle;
using TowerFall;

namespace OnlineMultiplayerMod.Model
{
    public static class EntityTypeWrappers
    {
        public static readonly ConcurrentDictionary<Type, TypeWrapper> EntityTypeWrappersDictionary =
            new ConcurrentDictionary<Type, TypeWrapper>();

        public static bool IsInstantiated { get; private set;}

        public static void Instantiate()
        {
            IEnumerable<Type> towerFallActors = Extensions.GetEnumerableOfTypes<LevelEntity>();

            Parallel.ForEach(towerFallActors, actor =>
            {
                EntityTypeWrappersDictionary.TryAdd(actor, new TypeWrapper(actor));
            });

            IsInstantiated = true;
        }
    }
}