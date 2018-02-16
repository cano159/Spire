using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using Harmony;
using TowerFall;
using static Spire.SpireController;

namespace Spire.Patches.MainMenu
{
    internal class MainMenuAddOptionsButtonsPatch : SpirePatch
    {
        public static MethodInfo TargetMethod =
            typeof(TowerFall.MainMenu).GetMethod("InitOptions", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void Prefix(TowerFall.MainMenu __instance, ref List<OptionsButton> buttons)
        {
            Dictionary<Mod, ConcurrentBag<OptionsButton>>.ValueCollection commands =
                Instance.OptionsButtonRegistrar.FromActive().Values;

            foreach (ConcurrentBag<OptionsButton> commandsList in commands)
                buttons.AddRange(commandsList);
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPrefix(harmony, TargetMethod);
        }
    }
}