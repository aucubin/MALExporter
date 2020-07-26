using System;
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
        private readonly Dictionary<string, string> _fieldsToParseRename;
        private readonly bool hasRenaming;

        private XmlParser(string filePath, string elementTag)
        {
            FilePath = filePath;
            ElementTag = elementTag;
            _xmlFile = new XmlDocument();
        }

        public XmlParser(string filePath, string elementTag, IEnumerable<string> fieldsToParse) : this(filePath, elementTag)
        {
            _fieldsToParse = new HashSet<string>(fieldsToParse);
            ParsedXml = new RepresentationList(fieldsToParse);
            hasRenaming = false;
        }

        public XmlParser(string filePath, string elementTag, IEnumerable<Tuple<string,string>> fieldsToParse) : this(filePath, elementTag)
        {
            _fieldsToParseRename = new Dictionary<string, string>();
            foreach(Tuple<string, string> tuple in fieldsToParse)
            {
                _fieldsToParseRename.Add(tuple.Item1, tuple.Item2);
            }
            List<string> tmpFields = new List<string>();
            foreach(Tuple<string,string> fields in fieldsToParse)
            {
                tmpFields.Add(fields.Item2);
            }
            ParsedXml = new RepresentationList(tmpFields);
            hasRenaming = true;
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
                        if (!hasRenaming && _fieldsToParse.Contains(childNode.Name))
                        {
                            currentRepresentation[childNode.Name].StringValue = childNode.InnerText;
                        }
                        else if(hasRenaming && _fieldsToParseRename.TryGetValue(childNode.Name, out string renamingName))
                        {
                            currentRepresentation[renamingName].StringValue = childNode.InnerText;
                        }
                    }
                }
            }
        }
    }
}
