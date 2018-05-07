using System;
using Monocle;
using Spire.Command;

namespace MiscAdditions.Commands
{
    /// <summary>
    /// A simple console command that allows the user to play, pause, and stop background music. 
    /// </summary>
    public class MusicConsoleCommand : ConsoleCommand
    {
        //Store the previous song here so we can resume it after we pause.
        private string _previousSong;

        //Base constructor.
        public MusicConsoleCommand() : base("music")
        {
        }

        /// <summary>
        /// Attempts to play the specified song from the song title. Will log any exceptions to the game's console upon invoke.
        /// </summary>
        /// <param name="songTitle">The song title</param>
        private void PlaySong(string songTitle)
        {
            try
            {
                //Attempt to play song.
                Music.Play(songTitle);
                Log($"Playing {songTitle}");
            }
            catch (Exception e)
            {
                //If the song wasn't found or some other exception occured, log it.
                Log($"{e.Message}");
            }
        }
        /// <summary>
        /// The console command's invoke method upon the command being entered. 
        /// </summary>
        /// <param name="args"></param>
        public override void Invoke(string[] args)
        {
            //if args is empty
            if (args.Length <= 0)
            {
                //show either the currently playing title or nothing.
                Log(Music.CurrentSong.Length > 0 ? $"Song: {Music.CurrentSong}" : "Not playing any music.");
            }
            else
            {
                foreach (string arg in args)
                {
                    switch (arg.ToLower())
                    {
                        case "play":
                            if (args.Length >= 1)
                                PlaySong(args[1]);
                            else
                                Music.Play(_previousSong);
                            break;
                        case "stop":
                            _previousSong = Music.CurrentSong;
                            Music.Stop();
                            break;
                    }

                }

            }
        }
    }
}