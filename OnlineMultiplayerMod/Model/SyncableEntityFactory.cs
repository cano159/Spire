using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TowerFall;

namespace OnlineMultiplayerMod.Model
{
    public class SyncableEntityFactory
    {
        public readonly ConcurrentDictionary<string, Type> ActorTypes = new ConcurrentDictionary<string, Type>();
        public readonly ConcurrentDictionary<Type, ConstructorInfo[]> ActorConstructors = new ConcurrentDictionary<Type, ConstructorInfo[]>();

        public SyncableEntityFactory()
        {
            LoadAllActorTypes();
        }

        private void LoadAllActorTypes()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            Assembly towerFallMainAssem = assemblies.First(x => x.GetName().ToString().Contains("TowerFall"));

            Parallel.ForEach(towerFallMainAssem.GetTypes(), type =>
            {
                if (!typeof(Actor).IsAssignableFrom(type)) 
                    return;

                ActorTypes.TryAdd(type.Name, type);

                ActorConstructors.TryAdd(type, type.GetConstructors());
            });
        }
    }
}
