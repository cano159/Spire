using Harmony;
using Microsoft.Xna.Framework;
using Spire.Events;
using TowerFall;

namespace Spire.Patches
{
    // ReSharper disable UnusedMember.Global
    [HarmonyPatch(typeof(RoundLogic), "OnPlayerDeath")]
    internal static class RoundLogicOnPlayerDeathPatch
    {
        public static void Postfix(Player player, PlayerCorpse corpse, int playerIndex, DeathCause cause,
            Vector2 position, int killerIndex)
        {
            EventController.Instance.PlayerDeath(player, corpse, playerIndex, cause, position, killerIndex);
        }
    }
}