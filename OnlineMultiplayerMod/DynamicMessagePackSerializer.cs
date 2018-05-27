using System;
using System.Collections.Concurrent;
using MsgPack.Serialization;

namespace OnlineMultiplayerMod
{
    public static class DynamicMessagePackSerializer
    {
        private static readonly ConcurrentDictionary<Type, MessagePackSerializer> Serializers =
            new ConcurrentDictionary<Type, MessagePackSerializer>();

        public static MessagePackSerializer Get(Type type)
        {
            if (Serializers.ContainsKey(type))
                return Serializers[type];

            Register(type);

            return Serializers[type];
        }

        public static bool Register(Type type)
        {
            return Serializers.TryAdd(type,
                MessagePackSerializer.Get(type, EnumSerializationMethod.ByUnderlyingValue));
        }
    }
}