using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace OnlineMultiplayerMod
{
    public static class Extensions
    {
        public static IEnumerable<Type> GetEnumerableOfTypes<T>()
        {
            return Assembly.GetAssembly(typeof(T))
                .GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)))
                .ToList();
        }
    }
}