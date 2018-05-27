using System;
using System.Linq;
using Harmony;
using Microsoft.Xna.Framework;
using Spire.Events;
using static Harmony.HarmonyInstance;

namespace Spire
{
    public abstract class Mod
    {
        protected virtual bool HarmonyAutoPatch => true;

        protected HarmonyInstance HarmonyInstance { get; set; }

        public abstract string ModName { get; }

        public abstract string ModAuthor { get; }

        public abstract string ModDescription { get; }

        public bool IsActive { get; internal set; }

        public abstract void OnModEnabled();
        public abstract void OnModDisabled();

        public abstract void Update(GameTime time);

        internal void ApplyHarmonyPatches()
        {
            if (!HarmonyAutoPatch)
                return;

            string harmonyId = $"{SpireController.HarmonyInstanceIdentifier}.{GetCorrectedModId()}";

            try
            {
                if (!SpireController.Instance.ShouldHarmonyAutoPatch(GetType().Assembly, harmonyId))
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

        internal string GetCorrectedModId()
        {
            return GetType().Assembly.GetName().Name.Replace(' ', '_');
        }
    }
}