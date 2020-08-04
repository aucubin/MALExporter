using Xunit;
using XmlParsing;
using System.Collections.Generic;
using InternalRepresentation;
using System.IO;

namespace UnitTests.XmlParsing
{
    public class XmlParserTests
    {
        private readonly string externalFileRootPath = "XmlParsing" + Path.DirectorySeparatorChar + "TestFiles" + Path.DirectorySeparatorChar;

        [Fact]
        public void SimpleXmlTest()
        {
            string path = Helpers.GetAbsolutePathForExternalUnitTestingFile(externalFileRootPath+"SimpleXmlTest.xml");
            HashSet<string> fields = new HashSet<string>() { "test" };
            XmlParser xml = new XmlParser(path, "child", fields);
            xml.ParseXML();


            Representation expectedRep = new Representation(fields);
            expectedRep["test"].StringValue = "TestValue";

            Assert.Equal(expectedRep, xml.ParsedXml[0]);
        }
    }
}
