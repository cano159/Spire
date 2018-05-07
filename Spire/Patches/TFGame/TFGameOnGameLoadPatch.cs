using System.Reflection;
using Harmony;
using Spire.Events;

namespace Spire.Patches.TFGame
{
    public class TfGameOnGameLoadedPatch : SpirePatch
    {
        public static MethodInfo SetGameLoadedMethod =
            typeof(TowerFall.TFGame).GetMethod("MainMenuLoadWait", BindingFlags.Public | BindingFlags.Static);

        private static bool wasAlreadyLoaded = false;

        public static void Postfix()
        {
            if (!wasAlreadyLoaded)
            {
                wasAlreadyLoaded = true;
                EventController.Instance.GameLoaded();
            }
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, SetGameLoadedMethod);
        }
    }
}