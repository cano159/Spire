using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;

namespace Spire.Patches.MainMenu
{
    public class MainMenuAddSpireVersionTextPatch : SpirePatch
    {
        public static readonly MethodInfo TargetMethod =
            typeof(TowerFall.MainMenu).GetMethod("Render", BindingFlags.Public | BindingFlags.Instance);

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var arr = instructions.ToArray();

            for (int counter = 0; counter < arr.Length; counter++)
            {
                switch (arr[counter].opcode)
                {
                    default:
                        yield return arr[counter];
                        break;
                }
            }
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchTranspiler(harmony, TargetMethod);
        }
    }
}
