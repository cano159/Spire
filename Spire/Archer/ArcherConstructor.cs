using System.Collections.Generic;
using System.Xml;
using Monocle;
using TowerFall;

namespace Spire.Archer
{
    public static class ArcherConstructor
    {
        public static IEnumerable<ArcherData> LoadArchersFromFile(string file)
        {
            XmlDocument xmlDocument = Calc.LoadXML(file);

            XmlNodeList xmlNodeList = xmlDocument["Archers"]?.ChildNodes;

            if (xmlNodeList == null) 
                yield break;

            foreach (object obj in xmlNodeList)
            {
                if (!(obj is XmlElement))
                    continue;
                var xmlElement = obj as XmlElement;

                yield return new ArcherData(xmlElement);
            }
        }
    }
}