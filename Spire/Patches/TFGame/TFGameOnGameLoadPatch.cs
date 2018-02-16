using System.Reflection;
using Harmony;
using Spire.Events;

namespace Spire.Patches.TFGame
{
    public class TfGameOnGameLoadPatch : SpirePatch
    {
        public static MethodInfo TargetMethod =
            typeof(TowerFall.TFGame).GetMethod("Load", BindingFlags.Public | BindingFlags.Static);

        public static void Postfix()
        {
            EventController.Instance.GameLoaded();
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, TargetMethod);
        }
    }
}