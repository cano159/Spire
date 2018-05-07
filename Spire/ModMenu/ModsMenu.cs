using Microsoft.Xna.Framework;
using Monocle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TowerFall;

namespace Spire.ModMenu
{
    public class ModsMenu : Scene
    {
        public IEnumerable<OptionsButton> Buttons;

        public ModsMenu(IEnumerable<Mod> mods)
        {
            Buttons = GetModMenuButtons(mods);
        }

        private IEnumerable<OptionsButton> GetModMenuButtons(IEnumerable<Mod> mods)
        {
            foreach (var mod in mods)
            {
                yield return GetModMenuButton(mod);
            }
        }

        private OptionsButton GetModMenuButton(Mod mod)
        {
            var modButton = new OptionsButton(mod.ModName.ToUpper());

            modButton.SetCallbacks(
                delegate { modButton.State = ExtensionMethods.BoolToString(mod.IsActive); }, 
                null, 
                null,
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

            return modButton;
        }

    }
}
