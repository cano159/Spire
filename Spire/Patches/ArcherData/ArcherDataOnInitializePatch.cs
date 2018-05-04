using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;

namespace Spire.Patches.ArcherData
{
    internal class ArcherDataOnInitializePatch : SpirePatch
    {
        public static MethodInfo TargetMethod =
            typeof(TowerFall.ArcherData).GetMethod("Initialize", BindingFlags.Public | BindingFlags.Static);

        public static void Postfix()
        {
            Dictionary<Mod, ConcurrentBag<TowerFall.ArcherData>>.ValueCollection addedVariants =
                SpireController.Instance.ArcherDataRegistrar.FromActive().Values;

            TowerFall.ArcherData.Archers.AddRangeToArray(addedVariants.SelectMany(bag => bag).ToArray());
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, TargetMethod);
        }
    }
}