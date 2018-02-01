using System;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using Spire;
using Spire.Settings;
using TowerFall;

namespace SpireModLoader
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                using (var game = new TFGame(SpireSettings.IsIntroDisabled))
                {
                    game.Graphics.PreparingDeviceSettings += (sender, e) =>
                        e.GraphicsDeviceInformation.GraphicsProfile = GraphicsProfile.HiDef;

                    TFGame.StartLoadLog();

                    SpireController.Instance.Initialize();

                    game.Run();
                }
            }
            catch (Exception e)
            {
                Engine.Instance.Exit();
                Console.WriteLine($"{e.Message}");
            }
        }
    }
}