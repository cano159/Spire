using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Harmony;
using Microsoft.Xna.Framework;

namespace Spire.Patches.TFGame
{
    public class TFGameOnDrawPatch : SpirePatch
    {
        public MethodInfo TargetMethod = typeof(TowerFall.TFGame).GetMethod("Draw", BindingFlags.NonPublic | BindingFlags.Instance);

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return instructions;
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchTranspiler(harmony, TargetMethod);
        }
    }
}
