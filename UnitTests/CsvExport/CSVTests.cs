using Xunit;
using System.IO;
using System;
using System.Collections.Generic;
using XmlParsing;
using Export.CSVExport;

namespace UnitTests.CsvExportTests
{
    public class CSVTests
    {
        private readonly string xmlFilePath = "XmlParsing" + Path.DirectorySeparatorChar + "TestFiles" + Path.DirectorySeparatorChar;
        private readonly string csvFilePath = "CsvExport" + Path.DirectorySeparatorChar + "TestFiles" + Path.DirectorySeparatorChar;

        [Theory]
        [InlineData("SimpleXmlTest","SimpleXmlTest")]
        [InlineData("MultipleChildsXmlTest","MultipleChildsXmlTest")]
        [InlineData("MultipleChildsWithDifferentChildren","MultipleChildsWithDifferentChildren")]
        public void ConvertXmlToCsvTest(string xmlFileName, string csvFileName)
        {
            string xmlFile = Helpers.GetAbsolutePathForExternalUnitTestingFile(xmlFilePath + xmlFileName + ".xml");
            string csvFile = Helpers.GetAbsolutePathForExternalUnitTestingFile(csvFilePath + csvFileName + ".csv");

            List<Tuple<string, string>> outFields = new List<Tuple<string, string>>();
            foreach (string field in XmlParser.GenerateListOfAllFields(xmlFile, "child"))
            {
                outFields.Add(new Tuple<string, string>(field, field));
            }

            XmlParser xml = new XmlParser(xmlFile, "child", outFields);

            xml.ParseXML();

            string actualFilePath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + Guid.NewGuid().ToString() + ".csv";

            CSVMain csv = new CSVMain(actualFilePath, false, xml.ParsedXml);

            csv.Export();

            string actualCSVData = File.ReadAllText(actualFilePath).Trim();
            string expectedCSVData = File.ReadAllText(csvFile).Trim();

            Assert.Equal(expectedCSVData, actualCSVData);

            File.Delete(actualFilePath);
        }
    }
}
