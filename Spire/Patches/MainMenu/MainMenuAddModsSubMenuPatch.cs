﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Harmony;

namespace Spire.Harmony.Patches.MainMenu
{
    public class MainMenuAddModsSubMenuPatch : SpirePatch
    {
        public static readonly Type TargetClass = typeof(TowerFall.MainMenu);

        public static readonly MethodInfo TargetMethod =
            TargetClass.GetMethod("Render", BindingFlags.Public | BindingFlags.Instance);

        public static IEnumerable<CodeInstruction> Transpiler(MethodBase original,
            IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
                yield return instruction;
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchTranspiler(harmony, TargetMethod);
        }
    }
}