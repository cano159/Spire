using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Monocle;

namespace Spire.Atlas
{
    public static class AtlasExtensionMethods
    {
        public static void Add(this Monocle.Atlas atlas, AtlasAdditions additions)
        {
            foreach (KeyValuePair<string, Subtexture> addition in additions.SubTextures)
            {
                atlas.SubTextures[addition.Key] = addition.Value;
            }
        }

        public static void AddRange(this Monocle.Atlas atlas, IEnumerable<AtlasAdditions> additions)
        {
            foreach (AtlasAdditions addition in additions)
            {
                Add(atlas, addition);
            }
        }
    }
}
