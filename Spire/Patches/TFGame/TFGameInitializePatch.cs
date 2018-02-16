using System.Reflection;
using Harmony;
using static Spire.Events.EventController;

namespace Spire.Patches.TFGame
{
    internal class TfGameInitializationPatch : SpirePatch
    {
        public static MethodInfo TargetMethod =
            typeof(TowerFall.TFGame).GetMethod("Initialize", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void Prefix()
        {
            Instance.GameInitializationStart();
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPrefix(harmony, TargetMethod);
        }
    }
}