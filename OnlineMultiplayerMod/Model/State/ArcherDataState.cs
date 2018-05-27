using Microsoft.Xna.Framework;
using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    internal class ArcherDataState
    {
        public bool RequiresDarkWorldDLC { get; }
        public ArcherData.BreathingInfo Breathing;
        public Color ColorA;
        public Color ColorB;
        public string Corpse;
        public ArcherData.GemInfo Gems;
        public TFGame.Genders Gender;
        public bool Hair;
        public Color LightbarColor;
        public string Name0;
        public string Name1;
        public bool PurpleParticles;
        public int SFXID;
        public int SleepHeadFrame;
        public bool StartNoHat;
        public string VictoryMusic;

        public ArcherDataState(ArcherData data)
        {
            RequiresDarkWorldDLC = data.RequiresDarkWorldDLC;
            Breathing = data.Breathing;
            ColorA = data.ColorA;
            ColorB = data.ColorB;
            Corpse = data.Corpse;
            Gems = data.Gems;
            Gender = data.Gender;
            Hair = data.Hair;
            LightbarColor = data.LightbarColor;
            Name0 = data.Name0;
            Name1 = data.Name1;
            PurpleParticles = data.PurpleParticles;
            SFXID = data.SFXID;
            SleepHeadFrame = data.SleepHeadFrame;
            StartNoHat = data.StartNoHat;
            VictoryMusic = data.VictoryMusic;
        }
    }
}