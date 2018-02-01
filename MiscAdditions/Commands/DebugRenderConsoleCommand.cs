using Spire.Command;

namespace MiscAdditions.Commands
{
    /// <inheritdoc />
    /// <summary>
    ///     A simple console command to unlock everything.
    /// </summary>
    public class DebugRenderConsoleCommand : ConsoleCommand
    {
        public DebugRenderConsoleCommand() : base("debug_render")
        {
        }

        public override void Invoke(string[] args)
        {
            switch (args[0]?.ToLower())
            {
                case "true":
                    MiscAdditionsMod.IsDebugModeEnabled = true;
                    Log("Debug rendering activated.");
                    break;
                case "false":
                    MiscAdditionsMod.IsDebugModeEnabled = false;
                    Log("Debug rendering disabled.");
                    break;
                default:
                    MiscAdditionsMod.IsDebugModeEnabled = !MiscAdditionsMod.IsDebugModeEnabled;
                    Log($"Debug rendering set to {MiscAdditionsMod.IsDebugModeEnabled}.");
                    break;
            }
        }
    }
}