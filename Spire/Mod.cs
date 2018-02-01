using System;
using System.Collections.Generic;
using Harmony;
using Microsoft.Xna.Framework;
using Monocle;
using Spire.Events;
using static Harmony.HarmonyInstance;

namespace Spire
{
    public abstract class Mod
    {
        public const string HarmonyInstancePrefix = "Spire.";

        protected virtual bool HarmonyAutoPatch => true;

        protected HarmonyInstance HarmonyInstance { get; set; }

        public abstract string ModName { get; }

        public virtual bool HasAtlasAddition => true;

        public bool IsActive { get; internal set; }

        public abstract void OnModLoad();

        public abstract void Update(GameTime time);

        internal void ApplyHarmonyPatches()
        {
            if (!HarmonyAutoPatch) return;

            string harmonyId = $"{HarmonyInstancePrefix}.{ModName}";

            try
            {
                if (!SpireController.Instance.ShouldHarmonyAutoPatch(GetType().Assembly, ModName))
                    return;

                HarmonyInstance = Create(harmonyId);
                HarmonyInstance.PatchAll(GetType().Assembly);
                EventController.Instance.OnGameUpdate += Instance_OnGameUpdate;
            }
            catch (Exception e)
            {
                Logger.Logger.LogExceptionOnLoad(e);
            }
        }

        private void Instance_OnGameUpdate(object sender, GameUpdatedEventArgs e)
        {
            if (IsActive)
                Update(e.Time);
        }
    }
}
