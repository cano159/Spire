using Microsoft.Xna.Framework;
using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class LevelSystemState
    {
        public Point Id { get; protected set; }
        public TowerThemeState Theme { get; protected set; }
        public bool ShowControls { get; protected set; }
        public bool ShowTriggerControls { get; protected set; }
        public bool Procedural { get; protected set; }
        public bool CustomTower { get; protected set; }

        public LevelSystemState(LevelSystem system)
        {
            Id = system.ID;
            Theme = new TowerThemeState(system.Theme);
            ShowControls = system.ShowControls;
            ShowTriggerControls = system.ShowTriggerControls;
            Procedural = system.Procedural;
            CustomTower = system.CustomTower;
        }
    }
}