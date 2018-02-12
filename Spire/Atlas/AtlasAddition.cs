using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Monocle;

namespace Spire.Atlas
{
    public class AtlasAddition
    {
        public Subtexture this[string name] => SubTextures[name];

        public Subtexture this[string name, Rectangle subRect] =>
            new Subtexture(this[name], subRect.X, subRect.Y, subRect.Width, subRect.Height);

        public readonly Dictionary<string, Subtexture> SubTextures = new Dictionary<string, Subtexture>();

        public string XmlPath { get; }

        public AtlasAddition(AtlasType type)
        {
            switch (type)
            {
                case AtlasType.Background:
                    XmlPath = TowerFall.TFGame.BGAtlas.XmlPath;
                    break;
                case AtlasType.Boss:
                    XmlPath = TowerFall.TFGame.BossAtlas.XmlPath;
                    break;
                case AtlasType.Editor:
                    XmlPath = TowerFall.TFGame.EditorAtlas.XmlPath;
                    break;
                case AtlasType.Main:
                    XmlPath = TowerFall.TFGame.Atlas.XmlPath;
                    break;
                case AtlasType.Menu:
                    XmlPath = TowerFall.TFGame.MenuAtlas.XmlPath;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
