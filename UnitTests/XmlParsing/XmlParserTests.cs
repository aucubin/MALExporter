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

        [Fact]
        public void MultipleChildsXmlTest()
        {
            string path = Helpers.GetAbsolutePathForExternalUnitTestingFile(externalFileRootPath + "MultipleChildsXmlTest.xml");
            HashSet<string> fields = new HashSet<string>() { "test", "test2" };
            XmlParser xml = new XmlParser(path, "child", fields);
            xml.ParseXML();

            RepresentationList expectedList = new RepresentationList(fields);
            Representation expectedRep1 = expectedList.CreateNewRepresentation();
            expectedRep1["test"].StringValue = "TestValue";
            expectedRep1["test2"].StringValue = "Party";
            Representation expectedRep2 = expectedList.CreateNewRepresentation();
            expectedRep2["test"].StringValue = "Lel";
            expectedRep2["test2"].StringValue = "kek";

            expectedList.Each((rep, i) =>
            {
                Assert.Equal(rep, xml.ParsedXml[i]);
            });
        }

        [Fact]
        public void MultipleChildsWithDifferentChildrenTest()
        {
            string path = Helpers.GetAbsolutePathForExternalUnitTestingFile(externalFileRootPath + "MultipleChildsWithDifferentChildren.xml");
            HashSet<string> fields = new HashSet<string>() { "test", "test2", "test3" };
            XmlParser xml = new XmlParser(path, "child", fields);
            xml.ParseXML();

            RepresentationList expectedList = new RepresentationList(fields);
            Representation expectedRep1 = expectedList.CreateNewRepresentation();
            expectedRep1["test"].StringValue = "TestValue";
            expectedRep1["test2"].StringValue = "Party";
            Representation expectedRep2 = expectedList.CreateNewRepresentation();
            expectedRep2["test"].StringValue = "Lel";
            expectedRep2["test3"].StringValue = "lelu";

            expectedList.Each((rep, i) =>
            {
                Assert.Equal(rep, xml.ParsedXml[i]);
            });
        }


        [Theory]
        [InlineData("SimpleXmlTest",new string[] { "test"})]
        [InlineData("MultipleChildsXmlTest",new string[] { "test", "test2"})]
        [InlineData("MultipleChildsWithDifferentChildren", new string[] { "test", "test2", "test3"})]
        public void GenerateListOfXmlFields(string fileName, string[] expectedFields)
        {
            string path = Helpers.GetAbsolutePathForExternalUnitTestingFile(externalFileRootPath + fileName + ".xml");
            HashSet<string> fields = new HashSet<string>(XmlParser.GenerateListOfAllFields(path, "child"));

            HashSet<string> expected = new HashSet<string>(expectedFields);

            foreach(string s in expected){
                Assert.Contains(s, fields);
            }
        }
    }
}
