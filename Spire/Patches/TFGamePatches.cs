using Harmony;
using Microsoft.Xna.Framework;
using Spire.Events;
using TowerFall;

namespace Spire.Patches
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Global

    [HarmonyPatch(typeof(TFGame), "Update")]
    internal static class TfGameOnGameUpdatePatch
    {
        public static void Prefix(ref GameTime gameTime)
        {
            EventController.Instance.GameUpdate(gameTime);
        }
    }

    [HarmonyPatch(typeof(TFGame), "Initialize")]
    internal static class TfGameInitializePatch
    {
        public static void Prefix()
        {
            EventController.Instance.GameInitializationStart();
        }
    }

    [HarmonyPatch(typeof(TFGame), "MainMenuLoadWait")]
    internal static class TfGameOnGameLoadPatch
    {
        private static bool _wasAlreadyLoaded;

        public static void Postfix()
        {
            if (_wasAlreadyLoaded)
                return;

            _wasAlreadyLoaded = true;
            EventController.Instance.GameLoaded();
        }
    }
}