using Microsoft.Xna.Framework;
using Spire.Events;
using System;
using System.Collections.Generic;
using TowerFall;
using System.Linq;
using System.Reflection;

namespace Spire.ModMenu
{
    public class SpireModsMenuMod : Mod
    {
        public override string ModName => "Spire Mods Menu";
        public override string ModAuthor => "ngrst183";
        public override string ModDescription => "Allows mods to be toggled on and off using the options menu";

        public override void OnModLoad()
        {
            EventController.Instance.OnMainMenuStateChange += Instance_OnMainMenuStateChange;
        }

        private void Instance_OnMainMenuStateChange(object sender, MainMenuStateEventArgs e)
        {
            if (e.State == MainMenu.MenuState.Options)
            {
                OptionsButton optionsButton = new OptionsButton("MODS");

                optionsButton.SetCallbacks(delegate 
                {
                    var modsMenu = new ModsMenu(e.MainMenu, GetModMenuButtons(SpireController.Instance.LoadedMods));
                });

                SpireController.Instance.OptionsButtonRegistrar.Add(this, optionsButton);
            }
        }

        private IEnumerable<OptionsButton> GetModMenuButtons(IEnumerable<Mod> mods)
        {
            foreach (var mod in mods.Where(x => !x.Equals(this)))
            {
                var modButton = new OptionsButton(mod.ModName.ToUpper());

                modButton.SetCallbacks(
                    delegate { modButton.State = ExtensionMethods.BoolToString(mod.IsActive); }, null, null,
                    delegate
                    {
                        if (mod.IsActive)
                        {
                            SpireController.Instance.DisableMod(mod);
                        }
                        else
                        {
                            SpireController.Instance.EnableMod(mod);
                        }

                        return mod.IsActive;
                    });

                yield return modButton;
            }
        }

        public override void Update(GameTime time)
        {
        }
    }
}
