using Npgsql;
using InternalRepresentation;

namespace Export.SQLExport
{
    public class SQLMain
    {
        private readonly string ConnectionString;
        private SQLTable SqlTable;

        public SQLMain(string connectionString, string tableName, Representation basisRepresentation)
        {
            ConnectionString = connectionString;
            SqlTable = new SQLTable(tableName, basisRepresentation);
        }

        private int ExecuteQuery(string query)
        {
            var conn = new NpgsqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(query, conn);
            int rows = cmd.ExecuteNonQuery();

            conn.Close();

            return rows;
        }

        public void CreateTable(bool overwrite = false)
        {
            ExecuteQuery(SqlTable.CreateTable(overwrite));
        }

        public int InsertTable(RepresentationList repList, bool overwrite = false)
        {
            return ExecuteQuery(SqlTable.InsertRepresentationList(repList,overwrite));
        }
    }
}
