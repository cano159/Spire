using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using MiscAdditions.Commands;
using Spire;
using static MiscAdditions.ExtensionMethods;
using static Monocle.Engine;
using Screen = Monocle.Screen;

namespace MiscAdditions
{
    public class MiscAdditionsMod : Mod
    {
        public override string ModName => "Misc Additions Mod";
        public override string ModAuthor => "ngrst183";
        public override string ModDescription =>
            "Adds random fixes and tweaks to the base game - including window resizing and additional console commands.";

        private TimeSpan _counterElapsed = TimeSpan.Zero;
        private int _fpsCounter;

        private readonly MethodInfo _setWindowSizeMethod =
            typeof(Screen).GetMethod("SetWindowSize", BindingFlags.NonPublic | BindingFlags.Instance);

        private readonly Form _window = Control.FromHandle(Instance.Window.Handle).FindForm();

        public override void OnModLoad()
        {
            SpireController.Instance.ConsoleCommandsRegistrar.Add(this, new UnlockEverythingCommand());
            SpireController.Instance.ConsoleCommandsRegistrar.Add(this, new MusicConsoleCommand());
            Instance.Window.AllowUserResizing = true;
            _window.SizeChanged += _window_SizeChanged;
        }

        private void _window_SizeChanged(object sender, EventArgs e)
        {
            if (_window.Width <= 0 || _window.Height <= 0 || Instance.Screen.IsFullscreen) return;

            _window.SizeChanged -= _window_SizeChanged;

            Console.WriteLine(
                $"Viewport Size: {Instance.GraphicsDevice.Viewport}, Window client bounds size : {Instance.Window.ClientBounds}");

            int boundsWidth = Instance.Window.ClientBounds.Width;
            int boundsHeight = Instance.Window.ClientBounds.Height;

            _setWindowSizeMethod.Invoke(Instance.Screen, new object[] {boundsWidth, boundsHeight});

            Instance.Graphics.PreferredBackBufferWidth = boundsWidth;
            Instance.Graphics.PreferredBackBufferHeight = boundsHeight;

            Instance.Graphics.ApplyChanges();

            _window.SizeChanged += _window_SizeChanged;
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