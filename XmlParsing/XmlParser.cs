using System.Collections.Generic;
using System.Xml;
using InternalRepresentation;

namespace XmlParsing
{
    public class XmlParser
    {
        private XmlDocument _xmlFile;

        public RepresentationList ParsedXml {
            get;
        }

        public readonly string FilePath;
        public readonly string ElementTag;
        private readonly HashSet<string> _fieldsToParse;

        public XmlParser(string filePath, string elementTag, IEnumerable<string> fieldsToParse)
        {
            FilePath = filePath;
            ElementTag = elementTag;
            _xmlFile = new XmlDocument();
            _fieldsToParse = new HashSet<string>(fieldsToParse);
            ParsedXml = new RepresentationList(fieldsToParse);
        }

        public void ParseXML()
        {
            _xmlFile.Load(FilePath);
            XmlNode rootNode = _xmlFile.DocumentElement;
            foreach(XmlNode listNode in rootNode.ChildNodes)
            {
                if(listNode.Name == ElementTag)
                {
                    Representation currentRepresentation = ParsedXml.CreateNewRepresentation();
                    foreach(XmlNode childNode in listNode.ChildNodes)
                    {
                        if (_fieldsToParse.Contains(childNode.Name))
                        {
                            currentRepresentation[childNode.Name].StringValue = childNode.InnerText;
                        }
                    }
                }
            }
        }
    }
}
