using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerFall;

namespace Spire.Arrow
{
    public class CustomArrow
    {
        public string Name { get; }
        public Color ColorA { get; }
        public Color ColorB { get; }

        public TowerFall.Arrow TowerFallArrow { get; }

        public CustomArrow(string name, Color colorA, Color colorB, TowerFall.Arrow towerFallArrow)
        {
            Name = name;
            ColorA = colorA;
            ColorB = colorB;
            TowerFallArrow = towerFallArrow;
        }
    }
}
