using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using Spire.Command;
using static Spire.SpireController;

namespace Spire.Patches.Commands
{
    public class CommandRegisterConsoleCommandsPatch : SpirePatch
    {
        public static ConstructorInfo TargetMethod = typeof(Monocle.Commands).GetConstructors().First();

        public static void Postfix(Monocle.Commands __instance)
        {
            var commands = Instance.ConsoleCommandsRegistrar.FromActive().Values;

            foreach (ConcurrentBag<ConsoleCommand> commandsList in commands)
                foreach (ConsoleCommand command in commandsList)
                    __instance.RegisterCommand(command.CommandString, command.Invoke);
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, TargetMethod);
        }
    }
}