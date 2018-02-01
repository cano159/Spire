using System.Reflection;
using Harmony;
using static Spire.Events.EventController;

namespace Spire.Patches.TFGame
{
    public class TFGameInitializationPatch : SpirePatch
    {
        public static MethodInfo TargetMethod = typeof(TowerFall.TFGame).GetMethod("Initialize", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void Prefix()
        {
            Instance.GameInitializationStart();
        }

        public static void Postfix()
        {
            SpireController.Instance.OnPostInitialize();
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchMultiple(harmony, TargetMethod, true, true, false);
        }
    }
}