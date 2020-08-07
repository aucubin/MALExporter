using CliFx;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.CommandTypesAndCLI
{
    public class CLITests
    {
        private readonly string XmlFilePath = "XmlParsing" + Path.DirectorySeparatorChar + "TestFiles" + Path.DirectorySeparatorChar;
        private readonly string ResultFilePath = "CommandTypesAndCLI" + Path.DirectorySeparatorChar + "TestFiles" + Path.DirectorySeparatorChar;

        [Theory]
        [InlineData("SimpleXmlTest","SimpleXmlTest")]
        [InlineData("MultipleChildsXmlTest","MultipleChildsXmlTest")]
        [InlineData("MultipleChildsWithDifferentChildren","MultipleChildsWithDifferentChildren")]
        public async Task ParseXmlFiles(string xmlFileName, string resultFileName)
        {
            var xmlPath = Helpers.GetAbsolutePathForExternalUnitTestingFile(XmlFilePath + xmlFileName + ".xml");
            var resultPath = Helpers.GetAbsolutePathForExternalUnitTestingFile(ResultFilePath + resultFileName + ".txt");

            await using var stdOut = new MemoryStream();
            var console = new VirtualConsole(output: stdOut);
            
            var app = new CliApplicationBuilder().AddCommand(typeof(MALExporter.Command)).UseConsole(console).Build();

            var args = new string[] {xmlPath, "child" };

            await app.RunAsync(args);

            var stdOutData = console.Output.Encoding.GetString(stdOut.ToArray());
            var expectedData = File.ReadAllText(resultPath);

            stdOutData = stdOutData.Trim();
            expectedData = expectedData.Trim();

            Assert.Equal(expectedData, stdOutData);
        }
    }
}
