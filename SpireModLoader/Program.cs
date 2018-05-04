using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Spire;
using Spire.Logger;
using Spire.Settings;
using TowerFall;
using static Monocle.Engine;

namespace SpireModLoader
{
    /// <summary>
    ///     The main class for SpireModLoader.
    /// </summary>
    internal class Program
    {
        private static void Main()
        {
            try
            {
                using (var game = new TFGame(SpireSettings.IsIntroDisabled))
                {
                    //Start debug load logging so that we can log any errors that occur in the controller to the loadlog.txt directly. 
                    TFGame.StartLoadLog();

                    //Initialize the SpireController
                    SpireController.Instance.Initialize();

                    //We have to do this because TowerFall crashes if this is not done.
                    game.Graphics.PreparingDeviceSettings += (sender, e) =>
                        e.GraphicsDeviceInformation.GraphicsProfile = GraphicsProfile.HiDef;

                    //Run the game.
                    game.Run();
                }
            }
            catch (Exception e)
            {
                //Exit the game upon exception occuring.
                Instance.Exit();

                Logger.LogException(e);

                //Also log any inner exceptions to console.
                if (e.InnerException != null)
                {
                    Logger.LogException(e.InnerException);
                }

                Console.ReadKey();
            }
        }
    }
}