using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Monocle;
using TowerFall;

namespace OnlineMultiplayerMod.Model
{
    public class SyncableEnemy : SyncableActor
    {
        [OverrideBase] public bool Aiming { get; protected set; }
        [OverrideBase] public float AimingAngle { get; protected set; }
        [OverrideBase] public bool Alive { get; protected set; }
        [OverrideBase] public EnemyArrowList Arrows { get; }
        [OverrideBase] public bool BouncesOnPlayer { get; protected set; }
        [OverrideBase] public int Bounty { get; }
        [OverrideBase] public bool CanBounceOn { get; protected set; }
        [OverrideBase] public int ColorIndex { get; protected set; }
        [OverrideBase] public virtual float DarkWorldDifficulty { get; protected set; }
        [OverrideBase] public bool Drilling { get; protected set; }
        [OverrideBase] public bool ExplosionsPush { get; protected set; }
        [OverrideBase] public bool Flaming { get; protected set; }
        [OverrideBase] public int Health { get; protected set; }
        [OverrideBase] public bool HoldsArrows { get; protected set; }
        [OverrideBase] public int KillerIndex { get; private set; }
        [OverrideBase] public int LaserBounceStart { get; protected set; }
        [OverrideBase] public int PreviousState { get; private set; }
        
        public SyncableEnemy(Enemy entity) : base(entity)
        {
            Aiming = entity.Aiming;
            AimingAngle = entity.AimingAngle;
            Alive = entity.Alive;
            Aiming = entity.Aiming;
            BouncesOnPlayer = entity.BouncesOnPlayer;
            CanBounceOn = entity.CanBounceOn;
            ColorIndex = entity.ColorIndex;
            Drilling = entity.Drilling;
            ExplosionsPush = entity.ExplosionsPush;
            Flaming = entity.Flaming;
            Health = entity.Health;
            HoldsArrows = entity.HoldsArrows;
            KillerIndex = entity.KillerIndex;
            LaserBounceStart = entity.LaserBounceStart;
            PreviousState = entity.PreviousState;
        }
    }
}