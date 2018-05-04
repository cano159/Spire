using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;

namespace Spire.Patches.MatchVariants
{
    internal class MatchVariantsSetCoopPatch : SpirePatch
    {
        public static ConstructorInfo TargetMethod =
            typeof(TowerFall.MatchVariants).GetConstructors().First();

        public static void Postfix(TowerFall.MatchVariants __instance)
        {
            foreach (var variant in __instance.Variants)
            {
                variant.CoOpValue = 1;
            }
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, TargetMethod);
        }
    }
}
