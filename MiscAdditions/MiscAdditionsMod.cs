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
        public override string ModName => "MiscAdditionsMod";

        internal static bool IsDebugModeEnabled { get; set; }

        private TimeSpan _counterElapsed = TimeSpan.Zero;
        private int _fpsCounter;

        private readonly Form _window = Control.FromHandle(Instance.Window.Handle).FindForm();

        public override void OnModLoad()
        {
            SpireController.Instance.ConsoleCommandsRegistrar.Add(this, new UnlockEverythingCommand());
            SpireController.Instance.ConsoleCommandsRegistrar.Add(this, new DebugRenderConsoleCommand());
            SpireController.Instance.ConsoleCommandsRegistrar.Add(this, new MusicConsoleCommand());

            Instance.Window.AllowUserResizing = true;
            Instance.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            Instance.Window.ClientSizeChanged -= Window_ClientSizeChanged;

            int width = Instance.Window.ClientBounds.Width;
            int height = Instance.Window.ClientBounds.Height;

            if (width <= 0 || height <= 0) return;

            Instance.Graphics.PreferredBackBufferHeight = height;
            Instance.Graphics.PreferredBackBufferWidth = width;
            Instance.Graphics.ApplyChanges();

            Instance.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        public override void Update(GameTime time)
        {
            _fpsCounter++;
            _counterElapsed += time.ElapsedGameTime;

            if (_counterElapsed >= TimeSpan.FromSeconds(1))
            {
                Instance.Window.Title =
                    $"TowerFall Ascension - {_fpsCounter} FPS - {SizeSuffix(Process.GetCurrentProcess().WorkingSet64)}";
                _fpsCounter = 0;
                _counterElapsed -= TimeSpan.FromSeconds(1);
            }

            if (_window.ControlBox) return;
            _window.ControlBox = true;
        }
    }
}