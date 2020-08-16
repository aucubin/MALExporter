using Xunit;
using InternalRepresentation;
using Export.SQLExport;
using System.Collections.Generic;

namespace Export.SQLExport
{
    public class SQLUnitTests
    {
        [Fact]
        public void CreateTableTest()
        {
            Representation rep = new Representation(new List<string>()
            {
                "test", "test2", "thirdfield"
            });

            SQLTable sql;
        }
    }
}
