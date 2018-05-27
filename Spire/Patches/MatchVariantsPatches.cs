using System.Collections.Concurrent;
using System.Collections.Generic;
using Harmony;
using TowerFall;

namespace Spire.Patches
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Global

    [HarmonyPatch(typeof(MatchVariants), new[] {typeof(bool)})]
    internal static class MatchVariantsAddVariantsPatch
    {
        public static void Prefix(MatchVariants __instance)
        {
            Dictionary<Mod, ConcurrentBag<Variant>>.ValueCollection variants =
                SpireController.Instance.VariantsRegistrar.FromActive().Values;

            foreach (ConcurrentBag<Variant> variantsBag in variants)
                __instance.Variants.AddRangeToArray(variantsBag.ToArray());
        }
    }
}