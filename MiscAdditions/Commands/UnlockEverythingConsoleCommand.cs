using Monocle;
using Spire.Command;
using TowerFall;

namespace MiscAdditions.Commands
{
    /// <inheritdoc />
    /// <summary>
    ///     A simple console command to unlock everything.
    /// </summary>
    public class UnlockEverythingCommand : ConsoleCommand
    {
        //Base constructor. The console command string is supplied here.
        public UnlockEverythingCommand() : base("unlock_everything")
        {
        }

        public override void Invoke(string[] args)
        {
            //Unlock everything.
            SaveData.Instance.Unlocks.UnlockAll();
            SaveData.Instance.Quest.RevealAll();

            //Unlock all DarkWorld DLC stuff if the user has it.
            if (GameData.DarkWorldDLC)
                SaveData.Instance.DarkWorld.RevealAll();

            SaveData.Instance.Unlocks.GunnStyle = true;
            SaveData.Instance.Unlocks.HandleVariantsAndAchievements(UnlockData.Unlocks.GunnStyle);
            //Save the game. 
            SaveData.Instance.Save();

            if (Engine.Instance.Scene is MainMenu mainMenu)
            {
                //Transition the background to the new blue background. 
                mainMenu.Background.AscensionTransition();

                if (Music.CurrentSong != "")
                {
                    //Play the new menu music.
                    MainMenu.PlayMenuMusic();
                }
            }

            //Log confirmation message to console.
            Log("Everything unlocked.");
        }
    }
}