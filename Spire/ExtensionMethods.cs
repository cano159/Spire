using System;
using System.Collections.Generic;
using System.Linq;

namespace Spire
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
        {
            return typeof(T)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract)
                .Select(t => (T) Activator.CreateInstance(t));
        }

        public static IEnumerable<Type> GetAllObjectsWithAttribute<T>()
        {
            return typeof(T)
                .Assembly.GetTypes()
                .Where(t => t.IsDefined(typeof(T), false));
        }

        public static string BoolToString(bool value)
        {
            return !value ? "OFF" : "ON";
        }
    }
}