using Harmony;
using Spire.Events;
using TowerFall;

namespace Spire.Patches
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Global

    [HarmonyPatch(typeof(Session), "StartGame")]
    internal static class SessionOnGameStartPatch
    {
        public static void Postfix(Session __instance)
        {
            EventController.Instance.SessionStart(__instance);
        }
    }
}