using Xunit;
using InternalRepresentation;
using System.Collections.Generic;
using Export.SQLExport;

namespace UnitTest.SqlExport
{
    public class SQLUnitTests
    {
        private static Representation CreateTestRepresentation()
        {
            Representation rep = new Representation(new List<string>()
            {
                "test", "test2", "thirdfield"
            });

            rep["test"].BoolValue = true;
            rep["test2"].StringValue = "hello";
            rep["thirdfield"].IntValue = -1;

            return rep;
        }

        private static void OverwriteTestRepresentation(ref Representation rep, bool testValue, string test2Value, int thirdfieldValue)
        {
            rep["test"].BoolValue = testValue;
            rep["test2"].StringValue = test2Value;
            rep["thirdfield"].IntValue = thirdfieldValue;
        }

        private static void CreateMultipleRepresentationsFromRepList(ref RepresentationList repList, int number)
        {
            for(int i = 0; i < number; i++)
            {
                repList.CreateNewRepresentation();
            }
        }

        [Fact]
        public void CreateTableTest()
        {
            SQLTable sql = new SQLTable("testTable", CreateTestRepresentation());

            string expectedCreateTableString = @"CREATE TABLE testTable \n (test boolean test2 text thirdfield integer);";

            Assert.Equal(expectedCreateTableString.Trim(), sql.CreateTable().Trim());
        }

        [Fact]
        public void InsertTableTest()
        {
            Representation rep = CreateTestRepresentation();
            RepresentationList repList = new RepresentationList(rep.Fields);
            CreateMultipleRepresentationsFromRepList(ref repList, 3);
            Representation firstItem = repList[0];
            Representation secondItem = repList[1];
            Representation thirdItem = repList[2];
            OverwriteTestRepresentation(ref firstItem, true, "help", -100);
            OverwriteTestRepresentation(ref secondItem, false, "nani", 200);
            OverwriteTestRepresentation(ref thirdItem, true, "lel", -212);

            SQLTable sql = new SQLTable("testTable", rep);

            string expectedInsertTableString = @"INSERT INTO testTable (test test2 thirdfield) VALUES \n (true 'help' -100),\n(false 'nani' 200),\n(true 'lel' -212);";

            Assert.Equal(expectedInsertTableString.Trim(), sql.InsertRepresentationList(repList).Trim());
        }
    }
}
