using System.Reflection;
using Harmony;
using Spire.Events;

namespace Spire.Patches.Session
{
    public class SessionOnGameStartPatch : SpirePatch
    {
        public static readonly MethodInfo TargetMethod = typeof(TowerFall.Session).GetMethod("StartGame", BindingFlags.Public | BindingFlags.Instance);

        public static void Postfix(TowerFall.Session __instance)
        {
            EventController.Instance.SessionStart(__instance);
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, TargetMethod);
        }
    }
}