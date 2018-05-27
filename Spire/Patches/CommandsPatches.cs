using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Harmony;
using Monocle;
using Spire.Command;
using Spire.Events;
using static Spire.SpireController;

namespace Spire.Patches
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Global

    [HarmonyPatch(typeof(Commands), "Log")]
    internal static class MonocleOnCommandLogPatch
    {
        public static void Postfix(Commands __instance, ref string str)
        {
            EventController.Instance.CommandLogFunction(str);
        }
    }

    [HarmonyPatch(typeof(Commands), "Trace")]
    internal static class MonocleOnCommandTracePatch
    {
        public static void Postfix(ref object obj)
        {
            EventController.Instance.CommandLogFunction(obj.ToString());
        }
    }

    [HarmonyPatch(typeof(Commands), new Type[] { })]
    internal static class MoncoleRegisterCommandsPatch
    {
        public static void Postfix(Commands __instance)
        {
            Dictionary<Mod, ConcurrentBag<ConsoleCommand>>.ValueCollection commands =
                Instance.ConsoleCommandsRegistrar.FromActive().Values;

            foreach (ConcurrentBag<ConsoleCommand> commandsList in commands)
            foreach (ConsoleCommand command in commandsList)
                __instance.RegisterCommand(command.CommandString, command.Invoke);
        }
    }

    [HarmonyPatch(typeof(Commands), "EnterCommand")]
    internal static class MonocleRemoveToLowerArgumentsPatch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeInstruction[] codeInstructions = instructions.ToArray();

            for (var counter = 0; counter < codeInstructions.Length; counter++)
                switch (counter)
                {
                    case 2:
                    case 3:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        continue;
                    default:
                        yield return codeInstructions[counter];
                        break;
                }
        }
    }
}