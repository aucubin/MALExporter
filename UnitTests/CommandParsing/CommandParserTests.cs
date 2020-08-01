using Xunit;
using MALExporter;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UnitTests.CommandParsing
{
    public class CommandParserTests
    {

        [Fact]
        public void EmptyCommaSeperatedListTest()
        {
            Assert.Throws<System.ArgumentException>(() => new CommaSeperatedList(string.Empty));
        }

        [Fact]
        public void EmptyCommaSeperatedListWithAssignmentTest()
        {
            Assert.Throws<System.ArgumentException>(() => new CommaSeperatedListWithAssignment(string.Empty));
        }

        [Fact]
        public void EmptyFieldsTest()
        {
            var CommandTest = new Command();

            Assert.Throws<System.ArgumentException>(() => CommandTest.Fields = new CommaSeperatedListWithAssignment(string.Empty));
        }

        [Theory]
        [InlineData("test", new string[] { "test" })]
        [InlineData("test,", new string[] { "test" })]
        [InlineData("test,test2", new string[] { "test", "test2" })]
        public void CommaSeperatedListTest(string infieldValue, string[] parsedInField)
        {
            var list = new CommaSeperatedList(infieldValue);

            Assert.Equal(parsedInField, list.ParsedList.ToArray());
        }

        [Theory]
        [InlineData("key=value", new string[] { "key" }, new string[] { "value" })]
        [InlineData("key=value,key2=value2", new string[] { "key", "key2" }, new string[] { "value", "value2" })]
        public void OutFieldsTest(string outfieldValue, string[] parsedOutFieldKey, string[] parsedOutFieldValue)
        {
            var CommandTest = new Command();

            CommandTest.Fields = new CommaSeperatedListWithAssignment(outfieldValue);

            List<string> keyList = new List<string>();
            List<string> valueList = new List<string>();

            foreach(Tuple<string,string> tuple in CommandTest.Fields.ParsedList)
            {
                keyList.Add(tuple.Item1);
                valueList.Add(tuple.Item2);
            }

            Assert.Equal(parsedOutFieldKey, keyList.ToArray());
            Assert.Equal(parsedOutFieldValue, valueList.ToArray());
        }
    }
}
