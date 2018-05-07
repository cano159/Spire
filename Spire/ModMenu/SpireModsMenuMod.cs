using Microsoft.Xna.Framework;
using Spire.Events;
using System;
using System.Collections.Generic;
using TowerFall;
using System.Linq;
using System.Reflection;
using System.Threading;
using Monocle;

namespace Spire.ModMenu
{
    public class SpireModsMenuMod : Mod
    {
        public override string ModName => "Spire Mods Menu";
        public override string ModAuthor => "ngrst183";
        public override string ModDescription => "Allows mods to be toggled on and off using the mods menu";

        public static MethodInfo TargetMethod =
            typeof(MainMenu).GetMethod("InitOptions", BindingFlags.NonPublic | BindingFlags.Instance);

        public static FieldInfo SchedulerField =
            typeof(MainMenu).GetField("scheduler", BindingFlags.NonPublic | BindingFlags.Instance);

        private List<OptionsButton> ModButtons = new List<OptionsButton>();

        private MainMenu mainMenu;

        public override void OnModLoad()
        {
            EventController.Instance.OnMainMenuStateChange += Instance_OnMainMenuStateChange;
            EventController.Instance.OnGameLoaded += Instance_OnGameLoaded;
        }

        private void Instance_OnGameLoaded(object sender, EventArgs e)
        {
            var modsButton = new OptionsButton("MODS");
            modsButton.SetCallbacks(delegate { OpenModsMenu(); });
            SpireController.Instance.OptionsButtonRegistrar.Add(this, modsButton);
        }

        private void Instance_OnMainMenuStateChange(object sender, MainMenuStateEventArgs e)
        {
            mainMenu = e.MainMenu;
        }

        public void OpenModsMenu()
        {
            var scheduler = SchedulerField.GetValue(mainMenu) as Scheduler;

            ModButtons = GetModMenuButtons(SpireController.Instance.LoadedMods).ToList();

            scheduler.ScheduleAction(RemoveOldMenuItems, 12, false);

            TargetMethod.Invoke(mainMenu, new object[] { ModButtons });

            mainMenu.UpdateEntityLists();

            TweenInOutDeselect();

            mainMenu.BackState = MainMenu.MenuState.Options;
        }

        private IEnumerable<OptionsButton> GetModMenuButtons(IEnumerable<Mod> mods)
        {
            foreach (var mod in mods.Where(x => x != this))
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

        private void RemoveOldMenuItems()
        {
            foreach (MenuItem menuItem in mainMenu.Layers[-1].GetList<MenuItem>())
                if (!ModButtons.Contains(menuItem))
                {
                    mainMenu.Remove(menuItem);
                }
        }
        private void TweenInOutDeselect()
        {
            foreach (MenuItem menuItem in mainMenu.Layers[-1].GetList<MenuItem>())
            {
                menuItem.Selected = false;
                if (menuItem.CreatedState == mainMenu.State)
                    menuItem.TweenIn();
                else if (menuItem.CreatedState == mainMenu.OldState)
                    menuItem.TweenOut();
            }
        }
        public override void Update(GameTime time)
        {
        }
    }
}
