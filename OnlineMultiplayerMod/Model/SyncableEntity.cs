using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using MsgPack.Serialization;
using TowerFall;

namespace OnlineMultiplayerMod.Model
{
    public class SyncableEntity<T> where T : LevelEntity
    {
        [MessagePackMember(0)] public string Type { get; }

        [MessagePackMember(1)] public long IterationNumber { get; private set; }

        [MessagePackMember(2)] public long Tick { get; set; }

        [MessagePackMember(3)] [OverrideBase] public Allegiance Allegiance { get; }

        [MessagePackMember(4)] [OverrideBase] public bool Seek { get; }

        [MessagePackMember(5)] [OverrideBase] public int SeekPriority { get; }

        [MessagePackMember(6)] [OverrideBase] public Vector2 SeekOffset { get; }

        [MessagePackMember(7)] [OverrideBase] public Vector2 Position { get; private set; }

        [MessagePackMember(8)] [OverrideBase] public float Width { get; }

        [MessagePackMember(9)] [OverrideBase] public float Height { get; }

        public Vector2 OldPosition { get; private set; }

        public T LocalEntity { get; protected set; }

        public int LerpCounter { get; private set; }

        private readonly List<PropertyInfo> _overridenProperties;

        protected SyncableEntity(T entity)
        {
            if (!EntityTypeWrappers.IsInstantiated)
                throw new InvalidOperationException(
                    "EntityTypeWrappers needs to be instantiated before creating a SyncableEntity.");

            _overridenProperties = GetType().GetProperties().Where(x => x.IsDefined(typeof(OverrideBase))).ToList();

            LocalEntity = entity;

            Type = entity.GetType().Name;
            IterationNumber = 0;
            Tick = 0;

            Position = OldPosition = entity.Position;

            Allegiance = LocalEntity.Allegiance;
            Seek = LocalEntity.Seek;
            SeekPriority = LocalEntity.SeekPriority;
            SeekOffset = LocalEntity.SeekOffset;

            Position = entity.Position;

            Width = entity.Width;
            Height = entity.Height;
        }

        public Delta<T> CreateDelta()
        {
            return new Delta<T>(LocalEntity);
        }

        public Delta<T> CreateDelta(Delta<T> delta)
        {
            if (delta == null) return CreateDelta();

            delta.IncrementDelta(LocalEntity);

            return delta;
        }

        public void UpdateFromDelta(Delta<T> delta)
        {
            LocalEntity = delta.ApplyTo(LocalEntity);
            IterationNumber = delta.IterationNumber;
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            OldPosition = Position;
            Position = newPosition;
            LerpCounter = 0;
        }

        public void Update()
        {
            foreach (PropertyInfo prop in _overridenProperties)
                SetEntityPropertyValue(prop.Name, prop.GetValue(LocalEntity));

            Vector2 interpolatedPosition = Lerp(OldPosition, Position, LerpCounter / 100f);

            SetEntityPropertyValue("Position", interpolatedPosition);

            Tick++;
            LerpCounter++;
        }

        public object GetEntityPropertyValue(string name)
        {
            return EntityTypeWrappers.EntityTypeWrappersDictionary[typeof(T)].Get(LocalEntity, name);
        }

        public void SetEntityPropertyValue(string name, object obj)
        {
            EntityTypeWrappers.EntityTypeWrappersDictionary[typeof(T)].Set(LocalEntity, name, obj);
        }

        private static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * by + secondFloat * (1 - by);
        }

        private static Vector2 Lerp(Vector2 firstVector, Vector2 secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Vector2(retX, retY);
        }
    }
}