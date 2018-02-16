using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Spire;
using TowerFall;
using static Monocle.Engine;
using static TowerFall.TFGame;

namespace MouseAimingMod
{
    public class MouseAimingMod : Mod
    {
        public override string ModName => "Mouse Aiming Mod";
        public override string ModAuthor => "ngrst183";
        public override string ModDescription => "Allows the use of the mouse for aiming and shooting arrows.";

        public override void OnModLoad()
        {
        }

        public override void Update(GameTime time)
        {
            if (!Instance.IsMouseVisible)
                Instance.IsMouseVisible = true;

            if (PlayerInputs.Length <= 0)
                return;

            foreach (PlayerInput input in GetKeyboardInputs())
            {
                int index = Array.IndexOf(PlayerInputs, input);
                PlayerInputs[index] = new MouseEnabledPlayerInput(input as KeyboardInput, index);
            }
        }

        private static IEnumerable<PlayerInput> GetKeyboardInputs()
        {
            return PlayerInputs.Where(x => x is KeyboardInput && !(x is MouseEnabledPlayerInput));
        }
    }
}