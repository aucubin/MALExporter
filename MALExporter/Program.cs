using InternalRepresentation;
using System;
using System.Collections.Generic;
using XmlParsing;

namespace MALExporter
{
    class Program
    {
        static void Main()
        {
            //RepresentationTests();
            HashSet<string> fieldsToParse = new HashSet<string>()
            {
                "manga_title",
                "manga_volumes",
                "manga_chapters",
                "my_read_volumes",
                "my_read_chapters",
                "my_score",
                "my_times_read"
            };
            XmlParser xml = new XmlParser(@"C:\Users\aucubin\Downloads\mangalist_1595159211_-_4711685.xml\mangalist_1595159211_-_4711685.xml", "manga", fieldsToParse);
            xml.ParseXML();
            foreach(var representations in xml.ParsedXml)
            {
                Console.WriteLine(representations.ToString());
            }

            List<string> testFields = new List<string>()
            {
                "test1",
                "test2",
                "test3"
            };

            Representation r1 = new Representation(testFields);
            Representation r2 = new Representation(testFields);

            Console.WriteLine(r1.Equals(r2));
        }
    }
}
