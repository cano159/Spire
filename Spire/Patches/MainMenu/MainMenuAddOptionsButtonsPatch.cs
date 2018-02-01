using System.Collections.Generic;
using System.Reflection;
using Harmony;
using Spire.Button;
using Spire.Command;
using static Spire.SpireController;

namespace Spire.Patches.MainMenu
{
    public class MainMenuAddOptionsButtonsPatch : SpirePatch
    {
        public static MethodInfo TargetMethod = typeof(TowerFall.MainMenu).GetMethod("InitOptions", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void Prefix(TowerFall.MainMenu __instance, ref List<TowerFall.OptionsButton> buttons)
        {
            var commands = Instance.OptionsButtonRegistrar.FromActive().Values;

            foreach (var commandsList in commands)
                buttons.AddRange(commandsList);
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPrefix(harmony, TargetMethod);
        }
    }
}
