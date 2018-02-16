using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using Spire.Atlas;
using static Spire.SpireController;

namespace Spire.Patches.Atlas
{
    internal class SpriteDataAdditionsPatch : SpirePatch
    {
        public static ConstructorInfo TargetMethod = typeof(Monocle.Atlas).GetConstructors().First();

        public static void Postfix(Monocle.Atlas __instance)
        {
            string atlasType = __instance.XmlPath;

            Dictionary<Mod, ConcurrentBag<AtlasAddition>>.ValueCollection additions =
                Instance.AtlasAdditionRegistrar.FromActive().Values;

            foreach (ConcurrentBag<AtlasAddition> additionsList in additions)
                __instance.AddRange(additionsList.Where(x => x.XmlPath == atlasType));
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, TargetMethod);
        }
    }
}