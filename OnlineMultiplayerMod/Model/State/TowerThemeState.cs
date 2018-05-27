using Microsoft.Xna.Framework;
using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class TowerThemeState
    {
        public string BackgroundData;
        public string BackgroundID;
        public string BGTileset;
        public bool Cataclysm;
        public bool Cold;
        public Color CrackedBlockColor;
        public Color DarknessColor;
        public float DarknessOpacity;
        public Color DrillParticleColor;
        public string ForegroundData;
        public float[] InvisibleOpacities;
        public TowerTheme.LanternTypes Lanterns;
        public Vector2 MapPosition;
        public string Music;
        public string Name;
        public bool Raining;
        public string Tileset;
        public MapButton.TowerType TowerType;
        public int Wind;
        public TowerTheme.Worlds World;

        public TowerThemeState(TowerTheme theme)
        {
            Name = theme.Name;
            Cataclysm = theme.Cataclysm;
            Raining = theme.Raining;
            CrackedBlockColor = theme.CrackedBlockColor;
            Cold = theme.Cold;
            DrillParticleColor = theme.DrillParticleColor;
            InvisibleOpacities = theme.InvisibleOpacities;
            BackgroundData = theme.BackgroundData.Value;
            BackgroundID = theme.BackgroundID;
            BGTileset = theme.BGTileset;
            ForegroundData = theme.ForegroundData.Value;
            World = theme.World;
            Tileset = theme.Tileset;
            MapPosition = theme.MapPosition;
            Music = theme.Music;
            TowerType = theme.TowerType;
            DarknessOpacity = theme.DarknessOpacity;
            Wind = theme.Wind;
            Lanterns = theme.Lanterns;
            DarknessColor = theme.DarknessColor;
        }
    }
}