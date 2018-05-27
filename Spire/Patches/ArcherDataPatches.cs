using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Harmony;
using TowerFall;

namespace Spire.Patches
{
    // ReSharper disable UnusedMember.Global

    [HarmonyPatch(typeof(ArcherData), "Initialize")]
    internal static class AddArcherDataPatch
    {
        public static void Postfix()
        {
            Dictionary<Mod, ConcurrentBag<ArcherData>>.ValueCollection addedVariants =
                SpireController.Instance.ArcherDataRegistrar.FromActive().Values;

            ArcherData.Archers.AddRangeToArray(addedVariants.SelectMany(bag => bag).ToArray());
        }
    }
}