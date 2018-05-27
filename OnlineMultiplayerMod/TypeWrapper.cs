using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

namespace OnlineMultiplayerMod
{
    public class TypeWrapper
    {
        private readonly Dictionary<string, CallSite<Func<CallSite, object, object>>> _getters
            = new Dictionary<string, CallSite<Func<CallSite, object, object>>>();

        private readonly Dictionary<string, CallSite<Action<CallSite, object, object>>> _setters
            = new Dictionary<string, CallSite<Action<CallSite, object, object>>>();

        public TypeWrapper(Type type)
        {
            foreach (PropertyInfo p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                string name = p.Name;
                CallSite<Action<CallSite, object, object>> set = CallSite<Action<CallSite, object, object>>.Create(
                    Binder.SetMember(
                        CSharpBinderFlags.None,
                        name,
                        type,
                        new[]
                        {
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                        }));

                _setters.Add(name, set);

                CallSite<Func<CallSite, object, object>> get = CallSite<Func<CallSite, object, object>>.Create(
                    Binder.GetMember(
                        CSharpBinderFlags.None,
                        name,
                        type,
                        new[] {CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)}));

                _getters.Add(name, get);
            }
        }

        public void Set(object objToSet, string name, object value)
        {
            CallSite<Action<CallSite, object, object>> set = _setters[name];
            set.Target(set, objToSet, value);
        }

        public object Get(object objToSet, string name)
        {
            CallSite<Func<CallSite, object, object>> get = _getters[name];
            return get.Target(get, objToSet);
        }
    }
}