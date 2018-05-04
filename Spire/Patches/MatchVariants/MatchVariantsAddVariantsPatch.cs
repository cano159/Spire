using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using TowerFall;

namespace Spire.Patches.MatchVariants
{
    internal class MatchVariantsAddVariantsPatch : SpirePatch
    {
        public static ConstructorInfo TargetMethod =
            typeof(TowerFall.MatchVariants).GetConstructors().First();

        public static void Prefix(TowerFall.MatchVariants __instance)
        {
            Dictionary<Mod, ConcurrentBag<TowerFall.Variant>>.ValueCollection variants = 
                SpireController.Instance.VariantsRegistrar.FromActive().Values;

            foreach (var variantsBag in variants)
                __instance.Variants.AddRangeToArray(variantsBag.ToArray());
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPrefix(harmony, TargetMethod);
        }
    }
}