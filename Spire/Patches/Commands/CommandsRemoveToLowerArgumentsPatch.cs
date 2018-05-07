using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;

namespace Spire.Patches.Commands
{
    internal class CommandsRemoveToLowerArgumentsPatch : SpirePatch
    {
        public static readonly MethodInfo EnterCommandMethod =
            typeof(Monocle.Commands).GetMethod("EnterCommand", BindingFlags.Instance | BindingFlags.NonPublic);

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            CodeInstruction[] codeInstructions = instructions.ToArray();

            for (var counter = 0; counter < codeInstructions.Length; counter++)
            {
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

        public override void Patch(HarmonyInstance harmony)
        {
            PatchTranspiler(harmony, EnterCommandMethod);
        }
    }
}