using Microsoft.Xna.Framework;
using OnlineMultiplayerMod.Model.State;
using TowerFall;

namespace OnlineMultiplayerMod.Model
{
    public class SyncablePlayer : SyncableActor
    {
        [OverrideBase] public int PlayerIndex { get; }
        [OverrideBase] public Facing Facing { get; }
        [OverrideBase] public bool OnGround { get; }
        [OverrideBase] public ArrowListState Arrows { get; }
        [OverrideBase] public bool Dead { get; }
        [OverrideBase] public Allegiance TeamColor { get; }
        [OverrideBase] public float AimDirection { get; }
        [OverrideBase]  public bool DrawSelf { get; }
        [OverrideBase] public Player.HatStates HatState { get; }
        [OverrideBase] public int Cling { get; }
        [OverrideBase]  public bool DodgeSliding { get; }
        [OverrideBase] public int CharacterIndex { get; }
        [OverrideBase] public ArcherData.ArcherTypes AltSelect { get; }
        [OverrideBase] public Player.PlayerStates State { get; }
        [OverrideBase] public bool HasSpeedBoots { get; }
        [OverrideBase] public bool Invisible { get; }
        [OverrideBase] public Vector2 Speed { get; }
        [OverrideBase] public int AmountOfTriggerArrowsActive { get; }

        public SyncablePlayer(Player obj) : base(obj)
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