using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Monocle;
using Spire;
using TowerFall;
using Component = Monocle.Component;
using Point = Microsoft.Xna.Framework.Point;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MouseKeyboardInputMod
{
    public class MouseKeyboardInputMod : Mod
    {
        public override string ModName => "Mouse + Keyboard Input Mod";
        public override string ModAuthor => "ngrst183";
        public override string ModDescription => "Allows the use of the mouse for navigating menus, aiming, and shooting.";

        private MenuItem _currentlySelectedMenuItem;

        public bool IsLeftMouseButtonBeingHeld;

        public static MethodInfo MenuInputOnConfirmMethod =
            typeof(MenuItem).GetMethod("OnConfirm", BindingFlags.NonPublic | BindingFlags.Instance);

        public static FieldInfo PauseMenuOptionNames =
            typeof(PauseMenu).GetField("optionNames", BindingFlags.NonPublic | BindingFlags.Instance);

        public static FieldInfo PauseMenuOptionActions =
            typeof(PauseMenu).GetField("optionActions", BindingFlags.NonPublic | BindingFlags.Instance);

        public override void OnModLoad()
        {
        }

        public override void Update(GameTime time)
        {
            if (!Engine.Instance.IsMouseVisible)
                Engine.Instance.IsMouseVisible = true;

            if (TFGame.PlayerInputs.Length <= 0)
                return;

            MouseState currentMouseState = Mouse.GetState();

            if (Engine.Instance.Scene is MainMenu mainMenu)
            {
                HandleMainMenuMouseInput(mainMenu, currentMouseState);

                switch (currentMouseState.LeftButton)
                {
                    case ButtonState.Pressed:
                        IsLeftMouseButtonBeingHeld = true;
                        break;

                    case ButtonState.Released when IsLeftMouseButtonBeingHeld:

                        if (_currentlySelectedMenuItem != null)
                        {
                            MenuInputOnConfirmMethod.Invoke(_currentlySelectedMenuItem, null);
                        }

                        IsLeftMouseButtonBeingHeld = false;

                        break;
                }
            }

            if (Engine.Instance.Scene is Level sceneAsLevel)
            {
                if (sceneAsLevel.Paused)
                {
                    HandlePauseMenuInput(sceneAsLevel, currentMouseState);
                }
            }

            foreach (PlayerInput input in GetKeyboardInputs())
            {
                int index = Array.IndexOf(TFGame.PlayerInputs, input);
                TFGame.PlayerInputs[index] = new MouseEnabledPlayerInput(input as KeyboardInput, index);
            }
        }

        private void HandlePauseMenuInput(Level level, MouseState currentMouseState)
        {
            _currentlySelectedMenuItem = null;

            var pauseMenu = GetCurrentPauseMenu(level);

            if (pauseMenu != null)
            {
                var menuPanel = pauseMenu.Components.First(x => x is MenuPanel) as MenuPanel;

                List<string> optionNames = PauseMenuOptionNames.GetValue(pauseMenu) as List<string>;

                var mouseStateRelative = GetScaledMousePosition(currentMouseState);
                
                foreach (var name in optionNames)
                {
                    int itemYPosition = (int)pauseMenu.Y + (optionNames.Count - optionNames.IndexOf(name));

                    Rectangle pauseMenuItemPosition = new Rectangle((int)pauseMenu.X - 30, itemYPosition, (int)menuPanel.Width, (int)menuPanel.Height);

                    if (pauseMenuItemPosition.Contains((int)mouseStateRelative.X, (int)mouseStateRelative.Y))
                    {

                    }
                }
            }
        }

        private PauseMenu GetCurrentPauseMenu(Level level)
        {
            foreach (var layer in level.Layers)
            {
                if (layer.Value.Contains<PauseMenu>())
                {
                    return layer.Value.GetFirst<PauseMenu>();
                }
            }

            return null;
        }

        private void HandleMainMenuMouseInput(MainMenu menu, MouseState currentMouseState)
        {
            _currentlySelectedMenuItem = null;

            if (menu.State == MainMenu.MenuState.Loading || menu.State == MainMenu.MenuState.None || menu.State == MainMenu.MenuState.Rollcall)
                return;

            var mouseStateRelative = GetScaledMousePosition(currentMouseState);

            foreach (Entity entity in menu.UILayer.Entities.Where(x => x is MenuItem))
            {
                var menuItem = (MenuItem)entity;

                if (IsMenuItemCurrentlyHoveredOver(menuItem, mouseStateRelative))
                {
                    _currentlySelectedMenuItem = menuItem;
                    menuItem.Selected = true;
                }
                else
                {
                    menuItem.Selected = false;
                }
            }
        }

        private bool IsMenuItemCurrentlyHoveredOver(MenuItem item, Vector2 scaledMouseCoordinates)
        {
            foreach (Component component in item.Components.Where(x => x is Image))
            {
                var image = (Image)component;

                Rectangle correctedRectangle = GetCorrectedBoundsRectangle(item, image);

                if (Engine.Instance.Screen.IsFullscreen)
                {
                    correctedRectangle.X -= (int)Engine.Instance.Screen.PadOffset;
                }

                if (correctedRectangle.Contains(new Point((int)scaledMouseCoordinates.X, (int)scaledMouseCoordinates.Y)))
                {
                    return true;
                }
            }

            return false;
        }

        private Rectangle GetCorrectedBoundsRectangle(Entity item, GraphicsComponent image)
        {
            Rectangle correctedRectangle;

            switch (item)
            {
                case MainModeButton mainModeButton:

                    correctedRectangle = new Rectangle(
                        (int) (mainModeButton.X - 20),
                        (int) (mainModeButton.Y - 20),
                        (int) (image.Width + 1.5),
                        (int) (image.Height + 1.5));
                    break;

                case BladeButton bladeButton:

                    correctedRectangle = new Rectangle(
                        (int) (bladeButton.X),
                        (int) (bladeButton.Y - 16),
                        (int) (image.Width + 1.5),
                        (int) (image.Height));

                    break;

                case OptionsButton optionsButton:

                    correctedRectangle = new Rectangle(
                        (int) optionsButton.X - 20, 
                        (int) optionsButton.Y - 10,
                        (int) image.Width * 6, 
                        (int) image.Height * 2);

                    break;

                default:
                    correctedRectangle = new Rectangle((int) item.Position.X, (int) item.Position.Y,
                        (int) image.Width, (int) image.Height);
                    break;
            }

            return correctedRectangle;
        }

        private Vector2 GetScaledMousePosition(MouseState state)
        {
            return new Vector2(state.X / Engine.Instance.Screen.Scale, state.Y / Engine.Instance.Screen.Scale);
        }

        private static IEnumerable<PlayerInput> GetKeyboardInputs()
        {
            return TFGame.PlayerInputs.Where(x => x is KeyboardInput && !(x is MouseEnabledPlayerInput));
        }
    }
}