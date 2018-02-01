using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Harmony;

namespace Spire.Harmony.Patches.MainMenu
{
    public class MainMenuDisplayStringPatch : SpirePatch
    {
        public static readonly Type TargetClass = typeof(TowerFall.MainMenu);

        public static readonly MethodInfo TargetMethod =
            TargetClass.GetMethod("Render", BindingFlags.Public | BindingFlags.Instance);

        public static bool Injected;

        public static IEnumerable<CodeInstruction> Transpiler(MethodBase original,
            IEnumerable<CodeInstruction> instructions)
        {
            var currentInstructionIndex = 0;

            foreach (CodeInstruction instruction in instructions)
            {
                yield return instruction;

                if (currentInstructionIndex != 54 || Injected)
                {
                    currentInstructionIndex++;
                    continue;
                }

                Injected = true;

                yield return new CodeInstruction(OpCodes.Call,
                    typeof(TowerFall.MainMenu).GetField("state", BindingFlags.NonPublic));
                yield return new CodeInstruction(OpCodes.Ldc_I4_S, TowerFall.MainMenu.MenuState.Options);
                yield return new CodeInstruction(OpCodes.Beq_S, OpCodes.Ldc_I4_1);
                yield return new CodeInstruction(OpCodes.Ldarg_0);

                yield return new CodeInstruction(OpCodes.Call,
                    typeof(TowerFall.MainMenu).GetField("state", BindingFlags.NonPublic));
                yield return new CodeInstruction(OpCodes.Ldc_I4_2, TowerFall.MainMenu.MenuState.PressStart);
                yield return new CodeInstruction(OpCodes.Ceq);
                yield return new CodeInstruction(OpCodes.Br_S, OpCodes.Stloc_1);
                yield return new CodeInstruction(OpCodes.Ldc_I4_1);
                yield return new CodeInstruction(OpCodes.Stloc_1);
                yield return new CodeInstruction(OpCodes.Brfalse_S, OpCodes.Ldarg_0);
            }
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchTranspiler(harmony, TargetMethod);
        }
    }
}