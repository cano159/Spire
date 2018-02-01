using System.Reflection;
using Harmony;
using Spire.Events;

namespace Spire.Patches.Scene
{
    public class SceneOnUpdatePatch : SpirePatch
    {
        public static MethodInfo TargetMethod = typeof(Monocle.Scene).GetMethod("Update", BindingFlags.Public | BindingFlags.Instance);

        public static void Postfix(Monocle.Scene __instance)
        {
            EventController.Instance.SceneUpdate(__instance);
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, TargetMethod);
        }
    }
}