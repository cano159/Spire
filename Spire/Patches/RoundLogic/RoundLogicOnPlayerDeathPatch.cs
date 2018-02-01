using System.Reflection;
using Harmony;
using Microsoft.Xna.Framework;
using Spire.Events;
using TowerFall;

namespace Spire.Patches.RoundLogic
{
    internal class RoundLogicOnPlayerDeathPatch : SpirePatch
    {
        public static MethodInfo TargetMethod = typeof(TowerFall.RoundLogic).GetMethod("OnPlayerDeath", BindingFlags.Public | BindingFlags.Instance);

        public static void Postfix(Player player, PlayerCorpse corpse, int playerIndex, DeathCause cause, Vector2 position, int killerIndex)
        {
            EventController.Instance.PlayerDeath(player, corpse, playerIndex, cause, position, killerIndex);
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, TargetMethod);
        }
    }
}