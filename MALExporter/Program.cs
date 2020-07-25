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
        }

        static void RepresentationTests()
        {
            var representation = new Representation(new List<string>()
            {
                "test", "test2"
            });

            representation["test"].StringValue = "test";
            representation["test2"].StringValue = "test2";

            foreach (var field in representation)
            {
                Console.WriteLine("Key: " + field.Key + ", Value: " + field.Value);
            }

            Console.WriteLine();

            var representationSet = new RepresentationList(new List<string>()
            {
                "test1",
                "test2",
                "test3"
            });

            var representation1 = representationSet.CreateNewRepresentation();
            representation1["test1"].StringValue = "test2";
            representation1["test3"].IntValue = -10;

            foreach (var field in representation1)
            {
                Console.WriteLine("Key: " + field.Key + ", Value: " + field.Value);
            }
        }
    }
}
