using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monocle;
using TowerFall;
using static Monocle.Engine;

namespace MouseAimingMod
{
    public class MouseEnabledPlayerInput : KeyboardInput
    {
        public override bool MenuConfirm => MInput.Keyboard.Pressed(_originalKeyboardInput.Config.Jump);
        public override bool MenuConfirmCheck => MInput.Keyboard.Check(_originalKeyboardInput.Config.Jump);
        public override bool MenuBack => MInput.Keyboard.Pressed(_originalKeyboardInput.Config.Shoot);
        public override bool MenuBackCheck => MInput.Keyboard.Check(_originalKeyboardInput.Config.Shoot);
        public override bool MenuStart => MInput.Keyboard.Pressed(_originalKeyboardInput.Config.Start);
        public override bool MenuSkipReplay => MInput.Keyboard.Pressed(_originalKeyboardInput.Config.Start);
        public override bool MenuStartCheck => MInput.Keyboard.Check(_originalKeyboardInput.Config.Start);
        public override bool MenuAlt => MInput.Keyboard.Pressed(_originalKeyboardInput.Config.Dodge);
        public override bool MenuAlt2 => MInput.Keyboard.Pressed(_originalKeyboardInput.Config.MenuAlt);
        public override bool MenuDown => MInput.Keyboard.Pressed(_originalKeyboardInput.Config.Down);
        public override bool MenuUp => MInput.Keyboard.Pressed(_originalKeyboardInput.Config.Up);
        public override bool MenuRight => MInput.Keyboard.Pressed(_originalKeyboardInput.Config.Right);
        public override bool MenuLeft => MInput.Keyboard.Pressed(_originalKeyboardInput.Config.Left);
        public override bool MenuUpCheck => MInput.Keyboard.Check(_originalKeyboardInput.Config.Up);
        public override bool MenuDownCheck => MInput.Keyboard.Check(_originalKeyboardInput.Config.Down);
        public override bool MenuLeftCheck => MInput.Keyboard.Check(_originalKeyboardInput.Config.Left);
        public override bool MenuRightCheck => MInput.Keyboard.Check(_originalKeyboardInput.Config.Right);
        public override bool MenuAltCheck => MInput.Keyboard.Check(_originalKeyboardInput.Config.Dodge);
        public override bool MenuAlt2Check => MInput.Keyboard.Check(_originalKeyboardInput.Config.MenuAlt);

        public override bool MenuSaveReplay => MenuAlt2;
        public override bool MenuSaveReplayCheck => MenuAlt2Check;

        public override Subtexture Icon => TFGame.MenuAtlas["controls/keyboard/icon"];

        public override Subtexture ConfirmIcon => _originalKeyboardInput.ConfirmIcon;
        public override Subtexture BackIcon => _originalKeyboardInput.BackIcon;
        public override Subtexture AltIcon => _originalKeyboardInput.AltIcon;
        public override Subtexture Alt2Icon => _originalKeyboardInput.Alt2Icon;
        public override Subtexture StartIcon => _originalKeyboardInput.StartIcon;
        public override Subtexture SkipReplayIcon => _originalKeyboardInput.SkipReplayIcon;
        public override Subtexture MoveIcon => null;
        public override Subtexture LeftIcon => _originalKeyboardInput.LeftIcon;
        public override Subtexture RightIcon => _originalKeyboardInput.RightIcon;
        public override Subtexture UpIcon => _originalKeyboardInput.UpIcon;
        public override Subtexture DownIcon => _originalKeyboardInput.DownIcon;
        public override Subtexture JumpIcon => _originalKeyboardInput.JumpIcon;
        public override Subtexture ShootIcon => _originalKeyboardInput.ShootIcon;
        public override Subtexture AltShootIcon => _originalKeyboardInput.AltShootIcon;
        public override Subtexture DodgeIcon => _originalKeyboardInput.Alt2Icon;
        public override Subtexture SaveReplayIcon => Alt2Icon;

        public override bool Attached => true;
        public override string Name => ID;
        public override string ID => "KEYBOARD + MOUSE";

        public readonly int PlayerIndex;
        private bool _isMouseButtonHeld;
        private Vector2 _oldMousePosition;
        private readonly KeyboardInput _originalKeyboardInput;

        public MouseEnabledPlayerInput(KeyboardInput input, int playerIndex)
        {
            PlayerIndex = playerIndex;
            _originalKeyboardInput = input;
        }

        public override InputState GetState()
        {
            InputState inputState = _originalKeyboardInput.GetState();
            MouseState mouseState = Mouse.GetState();

            switch (mouseState.LeftButton)
            {
                case ButtonState.Pressed:
                    _isMouseButtonHeld = true;
                    inputState.ShootPressed = true;
                    inputState.ShootCheck = true;
                    break;

                case ButtonState.Released when _isMouseButtonHeld:
                    _isMouseButtonHeld = false;
                    inputState.ShootPressed = false;
                    inputState.ShootCheck = false;
                    break;
            }

            Vector2 position = GetPlayerPositionFromInputIndex(PlayerIndex);

            var aimVector = new Vector2(mouseState.X / Instance.Screen.Scale - position.X,
                mouseState.Y / Instance.Screen.Scale - position.Y);

            inputState.AimAxis = aimVector;

            return inputState;
        }

        private Vector2 GetPlayerPositionFromInputIndex(int index)
        {
            IList<Entity> players = Instance.Scene[GameTags.Player];

            if (players.Count <= 0 || players[index] == null || !players[index].Active)
                return _oldMousePosition;

            var pos = new Vector2(players[index].X, players[index].Y);

            return _oldMousePosition = pos;
        }
    }
}