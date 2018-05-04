using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;
using Spire.Events;

namespace Spire.Patches.MainMenu
{
    public class MainMenuGetMenuStateChangedPatch : SpirePatch
    {
        public static MethodInfo SetStateMethod =
            typeof(TowerFall.MainMenu).GetMethod("set_State", BindingFlags.Public | BindingFlags.Instance);

        public static FieldInfo SwitchToField =
            typeof(TowerFall.MainMenu).GetField("switchTo", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void Postfix(TowerFall.MainMenu __instance)
        {
            EventController.Instance.MainMenuStateChanged(__instance, (TowerFall.MainMenu.MenuState)SwitchToField.GetValue(__instance));
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, SetStateMethod);
        }
    }
}
