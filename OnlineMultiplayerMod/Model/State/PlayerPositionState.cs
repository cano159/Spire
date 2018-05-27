using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class PlayerPositionState : EntityPositionState
    {
        public int PlayerIndex { get; }
        public Facing Facing { get; }
        public bool OnGround { get; }
        public ArrowListState Arrows { get; }
        public bool Dead { get; }
        public Allegiance TeamColor { get; }
        public float AimDirection { get; }
        public bool DrawSelf { get; }
        public Player.HatStates HatState { get; }
        public int Cling { get; }
        public bool DodgeSliding { get; }
        public int CharacterIndex { get; }
        public ArcherData.ArcherTypes AltSelect { get; }
        public Player.PlayerStates State { get; }
        public bool HasSpeedBoots { get; }
        public bool Invisible { get; }
        public Vector2 Speed { get; }
        public int AmountOfTriggerArrowsActive {get; }

        public PlayerPositionState(Player obj) : base(obj)
        {
            PlayerIndex = obj.PlayerIndex;
            Facing = obj.Facing;
            OnGround = obj.OnGround;
            Arrows = new ArrowListState(obj.Arrows);
            Dead = obj.Dead;
            TeamColor = obj.TeamColor;
            DrawSelf = obj.DrawSelf;
            HatState = obj.HatState;
            Cling = obj.Cling;
            DodgeSliding = obj.DodgeSliding;
            CharacterIndex = obj.CharacterIndex;
            AltSelect = obj.AltSelect;
            State = obj.State;
            HasSpeedBoots = obj.HasSpeedBoots;
            Invisible = obj.Invisible;
            Speed = obj.Speed;
            AimDirection = obj.AimDirection;
            AmountOfTriggerArrowsActive = obj.AmountOfTriggerArrowsActive;
        }
    }
}