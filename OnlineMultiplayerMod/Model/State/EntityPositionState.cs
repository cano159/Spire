using System;
using Monocle;

namespace OnlineMultiplayerMod.Model.State
{
    public class EntityPositionState
    {
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }

        public EntityPositionState(Entity obj)
        {
            X = Convert.ToInt32(obj.X);
            Y = Convert.ToInt32(obj.Y);
            Width = Convert.ToInt32(obj.Width);
            Height = Convert.ToInt32(obj.Height);
        }
    }
}