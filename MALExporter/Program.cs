using CliFx;
using CliFx.Attributes;
using Export.CSVExport;
using Export.SQLExport;
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
    }

    internal static class BaseCommands
    {
        internal static XmlParser GetArgumentsAndParseXML(CommaSeperatedListWithAssignment fields, string tag, string fileName)
        {
            IEnumerable<Tuple<string, string>> outFields;
            if (fields != null)
            {
                outFields = fields.ParsedList;
            }
            else
            {
                List<Tuple<string, string>> tmpList = new List<Tuple<string, string>>();
                foreach (string field in XmlParser.GenerateListOfAllFields(fileName, tag))
                {
                    tmpList.Add(new Tuple<string, string>(field, field));

                }
                outFields = tmpList;
            }

            XmlParser xml = new XmlParser(fileName, tag, outFields);

            xml.ParseXML();

            return xml;
        }
    }

    [Command("print", Description = "This command prints the XML Files to stdout")]
    public class PrintCommand : ICommand
    {
        [CommandParameter(0, Description = "Filename of input file")]
        public string FileName { get; set; }

        [CommandParameter(1, Description = "XML Tag to start parsing from")]
        public string Tag { get; set; }

        [CommandOption("fields", 'f', Description = "Optional comma seperated list with fields to read in and optional rewriting scheme")]
        public CommaSeperatedListWithAssignment Fields { get; set; }

        public ValueTask ExecuteAsync(IConsole console)
        {
            XmlParser xml = BaseCommands.GetArgumentsAndParseXML(Fields, Tag, FileName);
            console.Output.WriteLine(xml.ParsedXml.ToString());

            return default;
        }
    }


    [Command("csv", Description = "This command exports the XML File into an CSV File")]
    public class CSVCommand : ICommand
    {
        [CommandParameter(0, Description = "Filename of input file")]
        public string FileName { get; set; }

        [CommandParameter(1, Description = "XML Tag to start parsing from")]
        public string Tag { get; set; }

        [CommandParameter(2, Description = "Filename of csv to export into")]
        public string CSVFile { get; set; }

        [CommandOption("fields", 'f', Description = "Optional comma seperated list with fields to read in and optional rewriting scheme")]
        public CommaSeperatedListWithAssignment Fields { get; set; }

        [CommandOption("force", Description = "Overwrite output file if it already exists")]
        public bool OverwriteFile { get; set; }

        public ValueTask ExecuteAsync(IConsole console)
        {
            XmlParser xml = BaseCommands.GetArgumentsAndParseXML(Fields, Tag, FileName);

            CSVMain export = new CSVMain(CSVFile, OverwriteFile, xml.ParsedXml);
            export.Export();

            return default;
        }
    }

    [Command("sql", Description = "This command exports the XML File into an SQL Database")]
    public class SQLCommand : ICommand
    {
        [CommandParameter(0, Description = "Filename of input file")]
        public string FileName { get; set; }

        [CommandParameter(1, Description = "XML Tag to start parsing from")]
        public string Tag { get; set; }

        [CommandParameter(2, Description = "Connection String for PostgreSQL Database to export into")]
        public string SQLConnectionString { get; set; }

        [CommandParameter(3, Description = "Table name to export into")]
        public string TableName { get; set; }

        [CommandOption("fields", 'f', Description = "Optional comma seperated list with fields to read in and optional rewriting scheme")]
        public CommaSeperatedListWithAssignment Fields { get; set; }

        [CommandOption("force", Description = "Overwrite table and data if it already exists")]
        public bool OverwriteSQL { get; set; }

        public ValueTask ExecuteAsync(IConsole console)
        {
            XmlParser xml = BaseCommands.GetArgumentsAndParseXML(Fields, Tag, FileName);

            SQLMain export = new SQLMain(SQLConnectionString, TableName, xml.ParsedXml[0]);

            export.CreateTable(OverwriteSQL);
            int rows = export.InsertTable(xml.ParsedXml, OverwriteSQL);

            console.Output.WriteLine(string.Format("{0} rows inserted on export", rows));

            return default;
        }
    }
}
