using System;
using Monocle;

namespace Spire.Events
{
    public class SceneEventArgs : EventArgs
    {
        public Scene Scene { get; }

        public SceneEventArgs(Scene scene)
        {
            Scene = scene;
        }
    }
}