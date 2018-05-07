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
    public class MonocleOnCommandTracePatch : SpirePatch
    {
        public static MethodInfo MonocleCommandsTraceMethod =
            typeof(Monocle.Commands).GetMethod("Trace", BindingFlags.Public | BindingFlags.Static);

        public static void Postfix(ref object obj)
        {
            EventController.Instance.CommandLogFunction(obj.ToString());
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, MonocleCommandsTraceMethod);
        }
    }
}

