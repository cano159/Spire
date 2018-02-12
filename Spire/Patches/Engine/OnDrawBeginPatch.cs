using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Harmony;
using Spire.Events;

namespace Spire.Patches.Engine
{
    internal class OnDrawBeginPatch : SpirePatch
    {
        public static readonly MethodInfo TargetMethod =
            typeof(Monocle.Engine).GetMethod("Draw", BindingFlags.NonPublic | BindingFlags.Instance);

        public static readonly MethodInfo PreRenderOperand =
            typeof(Monocle.Scene).GetMethod("PreRender", BindingFlags.Instance | BindingFlags.Public);

        public static readonly MethodInfo BeginDrawMethod =
            typeof(EventController).GetMethod("DrawBegin", BindingFlags.Instance | BindingFlags.NonPublic);

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            /*
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Callvirt && (MethodInfo) instruction.operand == PreRenderOperand)
                    yield return new CodeInstruction(OpCodes.Call, BeginDrawMethod);
                else
                    yield return instruction;
            }
            */
            return instructions;
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchTranspiler(harmony, TargetMethod);
        }
    }
}