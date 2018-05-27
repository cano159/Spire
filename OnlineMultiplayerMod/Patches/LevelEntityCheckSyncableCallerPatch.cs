using System;
using Harmony;
using TowerFall;

namespace OnlineMultiplayerMod.Patches
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Global
    [HarmonyPatch(typeof(LevelEntity), "Update")]
    public static class LevelEntityCheckSyncableCallerPatch
    {
        [HarmonyPrefix]
        public static void Prefix(LevelEntity __instance)
        {
            Console.WriteLine($"{__instance.Active}");
        }
    }
}