using Harmony;
using Monocle;
using Spire.Events;

namespace Spire.Patches
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Global

    [HarmonyPatch(typeof(Scene), "Begin")]
    internal static class SceneOnBeginPatch
    {
        public static void Postfix(Scene __instance)
        {
            EventController.Instance.SceneBegin(__instance);
        }
    }

    [HarmonyPatch(typeof(Scene), "End")]
    internal static class SceneOnEndPatch
    {
        public static void Postfix(Scene __instance)
        {
            EventController.Instance.SceneEnd(__instance);
        }
    }

    [HarmonyPatch(typeof(Scene), "Update")]
    internal static class SceneOnUpdatePatch
    {
        public static void Postfix(Scene __instance)
        {
            EventController.Instance.SceneUpdate(__instance);
        }
    }
}