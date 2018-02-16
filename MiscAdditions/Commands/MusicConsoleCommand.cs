using System;
using Monocle;
using Spire.Command;

namespace MiscAdditions.Commands
{
    public class MusicConsoleCommand : ConsoleCommand
    {
        private string _previousSong;

        public MusicConsoleCommand() : base("music")
        {
        }

        private void PlaySong(string songTitle)
        {
            try
            {
                Music.Play(songTitle);
                Log($"Playing {songTitle}");
            }
            catch (Exception e)
            {
                Log($"{e.Message}");
            }
        }

        public override void Invoke(string[] args)
        {
            if (args.Length <= 0)
            {
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
                            break;
                        case "stop":
                            _previousSong = Music.CurrentSong;
                            Music.Stop();
                            break;
                        case "start":
                            Music.Play(_previousSong);
                            break;
                    }
                }
            }
        }
    }
}