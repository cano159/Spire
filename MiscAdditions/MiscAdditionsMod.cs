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

        public float OriginalScale { get; private set; }
        public float OriginalScaledHeight { get; private set; }
        public float OriginalScaledWidth { get; private set; }

        private TimeSpan _counterElapsed = TimeSpan.Zero;
        private int _fpsCounter;

        private readonly Form _window = Control.FromHandle(Instance.Window.Handle).FindForm();

        public override void OnModEnabled()
        {
            //Register our console commands with the spire controller.
            SpireController.Instance.ConsoleCommandsRegistrar.Add(this, new UnlockEverythingCommand());
            SpireController.Instance.ConsoleCommandsRegistrar.Add(this, new MusicConsoleCommand());
            SpireController.Instance.ConsoleCommandsRegistrar.Add(this, new SpawnEntityConsoleCommand());

            //Gets the scaled width, height and scale for resizing.
            OriginalScaledWidth = Instance.Screen.ScaledWidth;
            OriginalScaledHeight = Instance.Screen.ScaledHeight;
            OriginalScale = Instance.Screen.Scale;


            //Set event to track when a window is resized.
            _window.SizeChanged += _window_SizeChanged;

            //Enable the user to resize the game window. 
            Instance.Window.AllowUserResizing = true;
        }

        public override void OnModDisabled()
        {
            SpireController.Instance.ConsoleCommandsRegistrar.RevokeAll(this);

            OriginalScale = Instance.Screen.Scale;

            _window.SizeChanged -= _window_SizeChanged;

            Instance.Window.AllowUserResizing = false;
            _window.ControlBox = false;
        }

        private void _window_SizeChanged(object sender, EventArgs e)
        {
            _window.SizeChanged -= _window_SizeChanged;

            Instance.Screen.Scale = GetNewScale(_window.ClientRectangle.Width, _window.ClientRectangle.Height, Instance.Screen.ScaledWidth, Instance.Screen.ScaledHeight, Instance.Screen.Scale);

            _window.SizeChanged += _window_SizeChanged;
        }

        /// <summary>
        /// Gets the approximate new scale for the game window using the old screen size and scale
        /// </summary>
        /// <param name="screenWidth">The changed screen width</param>
        /// <param name="screenHeight">The changed screen height</param>
        /// <param name="scaledWidth">The old scaled width</param>
        /// <param name="scaledHeight">The old scaled height</param>
        /// <param name="originalScale">The original scale set by the game</param>
        /// <returns>The new scale</returns>
        private static float GetNewScale(int screenWidth, int screenHeight, int scaledWidth, int scaledHeight, float originalScale)
        {    
            //get the differences between the old and new sizes
            float differenceWidth = screenWidth - scaledWidth;
            float differenceHeight = screenHeight - scaledHeight;

            //get the approximate scale using the difference in scale and scaled width/height
            float scaledDifferenceWidth = originalScale / (scaledWidth / differenceWidth);
            float scaledDifferenceHeight = originalScale / (scaledHeight / differenceHeight);

            //Add the new scale to the old one. If the scale is smaller, the value will subtract from the existing scale. 
            return originalScale + scaledDifferenceWidth + scaledDifferenceHeight / originalScale;
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