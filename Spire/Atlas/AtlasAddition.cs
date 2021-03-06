﻿using System;
using System.Collections.Generic;
using System.Drawing;
using Monocle;
using TowerFall;

namespace Spire.Atlas
{
    public class AtlasAddition
    {
        public Subtexture this[string name] => SubTextures[name];

        public Subtexture this[string name, Rectangle subRect] =>
            new Subtexture(this[name], subRect.X, subRect.Y, subRect.Width, subRect.Height);

        public string XmlPath { get; }

        public readonly Dictionary<string, Subtexture> SubTextures = new Dictionary<string, Subtexture>();

        public AtlasAddition(AtlasType type)
        {
            switch (type)
            {
                case AtlasType.Background:
                    XmlPath = TFGame.BGAtlas.XmlPath;
                    break;
                case AtlasType.Boss:
                    XmlPath = TFGame.BossAtlas.XmlPath;
                    break;
                case AtlasType.Editor:
                    XmlPath = TFGame.EditorAtlas.XmlPath;
                    break;
                case AtlasType.Main:
                    XmlPath = TFGame.Atlas.XmlPath;
                    break;
                case AtlasType.Menu:
                    XmlPath = TFGame.MenuAtlas.XmlPath;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}