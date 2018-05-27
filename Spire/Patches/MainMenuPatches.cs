using System.Collections.Concurrent;
using System.Collections.Generic;
using Harmony;
using Spire.Events;
using TowerFall;

namespace Spire.Patches
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Global

    [HarmonyPatch(typeof(MainMenu), "InitOptions")]
    internal static class MainMenuAddOptionsButtonsPatch
    {
        public static void Prefix(MainMenu __instance, ref List<OptionsButton> buttons)
        {
            Dictionary<Mod, ConcurrentBag<OptionsButton>>.ValueCollection buttonsFromRegistrar =
                SpireController.Instance.OptionsButtonRegistrar.FromActive().Values;

            foreach (ConcurrentBag<OptionsButton> buttonsList in buttonsFromRegistrar) buttons.AddRange(buttonsList);
        }
    }

    [HarmonyPatch(typeof(MainMenu))]
    [HarmonyPatch("State", PropertyMethod.Setter)]
    internal static class MainMenuGetMenuStateChangedPatch
    {
        public static void Postfix(MainMenu __instance, ref MainMenu.MenuState value)
        {
            EventController.Instance.MainMenuStateChanged(__instance, value);
        }
    }
}