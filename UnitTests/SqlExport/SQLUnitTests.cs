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

        private static void CreateInsertTableTestData(out Representation rep, out RepresentationList repList)
        {
            rep = CreateTestRepresentation();
            repList = new RepresentationList(rep.Fields);
            CreateMultipleRepresentationsFromRepList(ref repList, 3);
            Representation firstItem = repList[0];
            Representation secondItem = repList[1];
            Representation thirdItem = repList[2];
            OverwriteTestRepresentation(ref firstItem, true, "help", -100);
            OverwriteTestRepresentation(ref secondItem, false, "nani", 200);
            OverwriteTestRepresentation(ref thirdItem, true, "lel", -212);
        }

        [Fact]
        public void CreateTableTest()
        {
            SQLTable sql = new SQLTable("testTable", CreateTestRepresentation());

            string expectedCreateTableString = @"CREATE TABLE testTable (test boolean, test2 text, thirdfield integer);";

            Assert.Equal(expectedCreateTableString.Trim(), sql.CreateTable().Trim());
        }

        [Fact]
        public void InsertTableTest()
        {
            CreateInsertTableTestData(out Representation rep, out RepresentationList repList);

            SQLTable sql = new SQLTable("testTable", rep);

            string expectedInsertTableString = @"INSERT INTO testTable (test, test2, thirdfield) VALUES (true, 'help', -100), (false, 'nani', 200), (true, 'lel', -212);";

            Assert.Equal(expectedInsertTableString.Trim(), sql.InsertRepresentationList(repList).Trim());
        }

        [Fact]
        public void TruncateTableTest()
        {
            SQLTable sql = new SQLTable("testTable", CreateTestRepresentation());

            string expectedTruncateTableString = @"TRUNCATE testTable;";

            Assert.Equal(expectedTruncateTableString.Trim(), sql.TruncateTable().Trim());
        }

        [Fact]
        public void DropTableTest()
        {
            SQLTable sql = new SQLTable("testTable", CreateTestRepresentation());

            string expecetedDropTableTstring = @"DROP TABLE testTable;";

            Assert.Equal(expecetedDropTableTstring.Trim(), sql.DropTable().Trim());
        }

        [Fact]
        public void InsertTableWithTruncateTest()
        {
            CreateInsertTableTestData(out Representation rep, out RepresentationList repList);

            SQLTable sql = new SQLTable("testTable", rep);

            string expectedInsertTableString = @"TRUNCATE testTable; INSERT INTO testTable (test, test2, thirdfield) VALUES (true, 'help', -100), (false, 'nani', 200), (true, 'lel', -212);";

            Assert.Equal(expectedInsertTableString.Trim(), sql.InsertRepresentationList(repList, true).Trim());
        }
    }
}
