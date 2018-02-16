using System.Reflection;
using Harmony;
using Microsoft.Xna.Framework;
using Spire.Events;

namespace Spire.Patches.TFGame
{
    internal class TfGameOnGameUpdatePatch : SpirePatch
    {
        public static MethodInfo TargetMethod =
            typeof(TowerFall.TFGame).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void Prefix(ref GameTime gameTime)
        {
            EventController.Instance.GameUpdate(gameTime);
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPrefix(harmony, TargetMethod);
        }
    }
}