using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerFall;

namespace OnlineMultiplayerMod.Model.State
{
    public class ArrowListState
    {
        public int MaxArrows { get; private set; }
        public List<ArrowTypes> Arrows { get; }
        public int Count { get; }
        public bool HasArrows { get; }

        public ArrowListState(ArrowList arrowList)
        {
            MaxArrows = arrowList.MaxArrows;
            Arrows = arrowList.Arrows;
            Count = arrowList.Count;
            HasArrows = arrowList.HasArrows;
        }
    }
}
