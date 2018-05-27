using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Harmony;
using Spire.Atlas;

namespace Spire.Patches
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Global

    [HarmonyPatch(typeof(Monocle.Atlas), new[] {typeof(string), typeof(string), typeof(bool)})]
    internal static class AddAtlasAdditionsPatch
    {
        public static void Postfix(Monocle.Atlas __instance)
        {
            string atlasType = __instance.XmlPath;

            Dictionary<Mod, ConcurrentBag<AtlasAddition>>.ValueCollection additions =
                SpireController.Instance.AtlasAdditionRegistrar.FromActive().Values;

            foreach (ConcurrentBag<AtlasAddition> additionsList in additions)
                __instance.AddRange(additionsList.Where(x => x.XmlPath == atlasType));
        }
    }
}