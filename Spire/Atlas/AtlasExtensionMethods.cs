using System.Collections.Generic;
using Monocle;

namespace Spire.Atlas
{
    public static class AtlasExtensionMethods
    {
        public static void Add(this Monocle.Atlas atlas, AtlasAddition additions)
        {
            foreach (KeyValuePair<string, Subtexture> addition in additions.SubTextures)
                atlas.SubTextures[addition.Key] = addition.Value;
        }

        public static void AddRange(this Monocle.Atlas atlas, IEnumerable<AtlasAddition> additions)
        {
            foreach (AtlasAddition addition in additions)
                Add(atlas, addition);
        }
    }
}