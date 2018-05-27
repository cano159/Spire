using Microsoft.Xna.Framework;
using MsgPack.Serialization;
using TowerFall;

namespace OnlineMultiplayerMod.Model
{
    public class SyncableActor : SyncableEntity<Actor>
    {
        [OverrideBase] public Vector2 ActualPosition { get; }
        [OverrideBase] public virtual bool FinishPushOnSquishRiding { get; }
        [OverrideBase] public bool FinishPushOnSquish { get; }
        [OverrideBase] public bool IgnoreJumpThrus { get; }
        [OverrideBase] public bool NaivePush { get; }
        [OverrideBase] public bool Pushable { get; }

        public SyncableActor(Actor entity) : base(entity)
        {
            ActualPosition = entity.ActualPosition;
            FinishPushOnSquishRiding = entity.FinishPushOnSquishRiding;
            FinishPushOnSquish = entity.FinishPushOnSquish;
            IgnoreJumpThrus = entity.IgnoreJumpThrus;
            NaivePush = entity.NaivePush;
            Pushable = entity.Pushable;
        }
    }
}