using System.Collections.Generic;
using System.Xml;
using Monocle;

namespace Spire.Archer
{
    public static class ArcherConstructor
    {
        public static IEnumerable<TowerFall.ArcherData> LoadArchersFromFile(string file)
        {
            XmlDocument xmlDocument = Calc.LoadXML(file);

            foreach (object obj in xmlDocument["Archers"].ChildNodes)
            {
                if (!(obj is XmlElement))
                    continue;
                var xmlElement = obj as XmlElement;

                yield return new TowerFall.ArcherData(xmlElement);
            }
        }
    }
}
