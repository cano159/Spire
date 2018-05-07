using Harmony;
using Monocle;
using Spire.Events;
using Spire.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Spire.Patches
{
    public class MonocleOnCommandLogPatch : SpirePatch
    {
        public static MethodInfo MonocleCommandsLogMethod =
            typeof(Monocle.Commands).GetMethod("Log", BindingFlags.Public | BindingFlags.Instance);

        public static void Postfix(Monocle.Commands __instance, ref string str)
        {
            EventController.Instance.CommandLogFunction(str);
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, MonocleCommandsLogMethod);
        }
    }
}

