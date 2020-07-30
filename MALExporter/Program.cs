using CliFx;
using CliFx.Attributes;
using InternalRepresentation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XmlParsing;

namespace MALExporter
{
    public class Program
    {
        public static async Task<int> Main() =>
            await new CliApplicationBuilder().AddCommandsFromThisAssembly().Build().RunAsync();

        public static void TestMain()
        {
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

            List<Tuple<string, string>> fieldsToParseRename = new List<Tuple<string, string>>()
            {
                new Tuple<string,string>("manga_title","Title"),
                new Tuple<string,string>("manga_volumes","Volumes"),
                new Tuple<string,string>("manga_chapters","Chapters"),
                new Tuple<string,string>("my_read_volumes","Read Volumes"),
                new Tuple<string,string>("my_read_chapters","Read Chapters"),
                new Tuple<string,string>("my_score","Score"),
                new Tuple<string,string>("my_times_read","Times Read")
            };
            XmlParser xml;
            bool rename = true;
            string path = @"C:\Users\aucubin\Downloads\mangalist_1595159211_-_4711685.xml\mangalist_1595159211_-_4711685.xml";
            if (rename)
            {
                xml = new XmlParser(path, "manga", fieldsToParseRename);
            }
            else
            {
                xml = new XmlParser(path, "manga", fieldsToParse);
            }
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

    [Command]
    public class Command : ICommand
    {
        public ValueTask ExecuteAsync(IConsole console)
        {
            Program.TestMain();

            return default;
        }
    }
}
