using CliFx;
using CliFx.Attributes;
using Export;
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

        internal static void TestMain()
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
            Console.WriteLine(xml.ParsedXml.ToString());

            List<string> testFields = new List<string>()
            {
                "test1",
                "test2",
                "test3"
            };

            Representation r1 = new Representation(testFields);
            Representation r2 = new Representation(testFields);

            Console.WriteLine(r1.Equals(r2));

            IEnumerable<string> test = XmlParser.GenerateListOfAllFields(path, "manga");

            foreach(string s in test)
            {
                Console.WriteLine(s);
            }
        }
    }

    [Command]
    public class Command : ICommand
    {
        [CommandParameter(0, Description = "Filename of input file")]
        public string FileName { get; set; }

        [CommandParameter(1, Description = "XML Tag to start parsing from")]
        public string Tag { get; set; }

        [CommandOption("csv", Description = "Filename of csv to export into")]
        public string CSVFile { get; set; }

        [CommandOption("fields", 'f', Description = "Optional comma seperated list with fields to read in and optional rewriting scheme")]
        public CommaSeperatedListWithAssignment Fields { get; set; }

        [CommandOption("force", Description = "Overwrite output file if it already exists")]
        public bool OverwriteFile { get; set; }

        public ValueTask ExecuteAsync(IConsole console)
        {
            IEnumerable<Tuple<string, string>> outFields;
            if (Fields != null)
            {
                outFields = Fields.ParsedList;
            }
            else
            {
                List<Tuple<string, string>> tmpList = new List<Tuple<string, string>>();
                foreach (string field in XmlParser.GenerateListOfAllFields(FileName, Tag))
                {
                    tmpList.Add(new Tuple<string, string>(field, field));

                }
                outFields = tmpList;
            }

            XmlParser xml = new XmlParser(FileName, Tag, outFields);

            xml.ParseXML();

            if (CSVFile != null)
            {
                CSVExport export = new CSVExport(CSVFile, OverwriteFile, xml.ParsedXml);
                export.Export();
            }
            else
            {
                console.Output.WriteLine(xml.ParsedXml.ToString());
            }

            return default;
        }
    }

    [Command("test")]
    public class TestCommand : ICommand
    {
        public ValueTask ExecuteAsync(IConsole console)
        {
            Program.TestMain();
            return default;
        }
    }
}
