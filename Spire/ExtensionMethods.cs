using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public static IEnumerable<Type> GetAllObjectsWithAttribute(Assembly assembly, Type type)
        {
            return type.Assembly.GetTypes().Where(t => t.IsDefined(type, false));
        }


        public static string BoolToString(bool value)
        {
            return !value ? "OFF" : "ON";
        }

        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

    }
}