using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;
using Spire.Atlas;
using Spire.Command;
using static Spire.SpireController;

namespace Spire.Patches.Atlas
{
    public class SpriteDataAdditionsPatch : SpirePatch
    {
        public static ConstructorInfo TargetMethod = typeof(Monocle.Atlas).GetConstructors().First();

        public static void Postfix(Monocle.Atlas __instance)
        {
            string atlasType = __instance.XmlPath;

            var additions = Instance.AtlasAdditionsRegistrar.FromActive().Values;

            foreach (var additionsList in additions)
                __instance.AddRange(additionsList.Where(x => x.XmlPath == atlasType));
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, TargetMethod);
        }
    }
}
