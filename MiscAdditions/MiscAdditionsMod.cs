using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using MiscAdditions.Commands;
using Spire;
using static MiscAdditions.ExtensionMethods;
using static Monocle.Engine;

namespace MiscAdditions
{
    public class MiscAdditionsMod : Mod
    {
        public override string ModName => "Misc Additions Mod";
        public override string ModAuthor => "ngrst183";

        public override string ModDescription =>
            "Adds random fixes and tweaks to the base game - including window resizing and additional console commands.";

        public float OriginalScale;
        public float OriginalScaledHeight;
        public float OriginalScaledWidth;

        private TimeSpan _counterElapsed = TimeSpan.Zero;
        private int _fpsCounter;

        private readonly Form _window = Control.FromHandle(Instance.Window.Handle).FindForm();

        public override void OnModLoad()
        {
            SpireController.Instance.ConsoleCommandsRegistrar.Add(this, new UnlockEverythingCommand());
            SpireController.Instance.ConsoleCommandsRegistrar.Add(this, new MusicConsoleCommand());

            OriginalScaledWidth = Instance.Screen.ScaledWidth;
            OriginalScaledHeight = Instance.Screen.ScaledHeight;

            OriginalScale = Instance.Screen.Scale;

            _window.SizeChanged += _window_SizeChanged;

            Instance.Window.AllowUserResizing = true;

        }

        private void _window_SizeChanged(object sender, EventArgs e)
        {
            if (_window.Width <= 0 || _window.Height <= 0 || Instance.Screen.Graphics.IsFullScreen 
                || Instance.Screen.DrawRect.Width == 0 || Instance.Screen.DrawRect.Height == 0 )
                return;

            _window.SizeChanged -= _window_SizeChanged;

            ResizeGameWindow();

            _window.SizeChanged += _window_SizeChanged;
        }

        private void ResizeGameWindow()
        {
            float diffWidth = _window.ClientRectangle.Width - Instance.Screen.ScaledWidth;
            float diffHeight = _window.ClientRectangle.Height - Instance.Screen.ScaledHeight;

            float scaleDifferenceWidth = OriginalScale / (Instance.Screen.ScaledWidth / diffWidth);
            float scaleDiffHeightWidth = OriginalScale / (Instance.Screen.ScaledHeight / diffHeight);

            OriginalScale += (scaleDifferenceWidth + scaleDiffHeightWidth / OriginalScale);

            Instance.Screen.Scale = OriginalScale;
        }

        public override void Update(GameTime time)
        {
            if (!_window.ControlBox)
                _window.ControlBox = true;

            _fpsCounter++;

            _counterElapsed += time.ElapsedGameTime;

            if (_counterElapsed < TimeSpan.FromSeconds(1)) return;

            Instance.Window.Title =
                $"TowerFall Ascension - {_fpsCounter} FPS - {SizeSuffix(Process.GetCurrentProcess().WorkingSet64)}";

            _fpsCounter = 0;
            _counterElapsed -= TimeSpan.FromSeconds(1);
        }
    }
}