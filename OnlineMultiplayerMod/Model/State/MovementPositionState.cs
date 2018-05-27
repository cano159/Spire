using System;
using Microsoft.Xna.Framework;
using Monocle;
using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class MovementPositionState : EntityPositionState
    {
        public bool Aiming { get; set; }
        public short Facing { get; set; }
        public Vector2 Speed { get; set; }
        public float AimingAngle { get; set; }
        public float AimDirection { get; set; }

        public MovementPositionState(Entity obj) : base(obj)
        {
            switch (obj)
            {
                case Enemy enemy:
                    Facing = Convert.ToInt16(enemy.Facing);
                    Aiming = enemy.Aiming;
                    Speed = enemy.Speed;
                    AimingAngle = enemy.AimingAngle;
                    break;
                case Player player:
                    Facing = Convert.ToInt16(player.Facing);
                    Aiming = player.Aiming;
                    Speed = player.Speed;
                    AimDirection = player.AimDirection;
                    break;
            }
        }
    }
}