using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using InternalRepresentation;

namespace Export.SQLExport
{
    public class SQLTable
    {
        private List<SQLColumn> RepresentationToSQLMapping;
        private readonly string TableName;

        private const string createStatement = @"CREATE TABLE {0} ({1});";
        private const string insertStatement = @"INSERT INTO {0} ({1}) VALUES {2};";
        private const string truncateStatement = @"TRUNCATE {0};";
        private const string dropStatement = @"DROP TABLE {0};";

        public SQLTable(string tableName, Representation basisRepresentation)
        {
            TableName = tableName;
            RepresentationToSQLMapping = new List<SQLColumn>();
            foreach(var field in basisRepresentation)
            {
                SQLColumn sql = new SQLColumn(field.Value, field.Key);
                RepresentationToSQLMapping.Add(sql);
            }
        }

        public string DropTable()
        {
            return string.Format(dropStatement, TableName);
        }

        public string CreateTable(bool dropTable = false)
        {
            StringBuilder sbColumns = new StringBuilder();
            foreach(var sqlMember in RepresentationToSQLMapping)
            {
                if(sbColumns.Length != 0)
                {
                    sbColumns.Append(", ");
                }
                sbColumns.Append(sqlMember.ColumnName);
                sbColumns.Append(" ");
                sbColumns.Append(sqlMember.SQLType);
            }

            string create = string.Format(createStatement, TableName, sbColumns.ToString());

            if (dropTable)
            {
                return string.Format("{0} {1}", DropTable(), create);
            }
            else
            {
                return create;
            }
        }

        public string InsertRepresentationList(RepresentationList repList, bool truncateTable = false)
        {
            if(repList.Count == 0)
            {
                throw new ArgumentException("RepresentationList to parse is empty. No SQL Insert possible");
            }

            StringBuilder sbColumn = new StringBuilder();

            foreach(var column in RepresentationToSQLMapping)
            {
                if(sbColumn.Length != 0)
                {
                    sbColumn.Append(", ");
                }
                sbColumn.Append(column.ColumnName);
            }

            StringBuilder sbValues = new StringBuilder("(");
            foreach (var rep in repList)
            {
                if(sbValues.Length != 1)
                {
                    sbValues.Append(@", (");
                }
                bool firstMember = true;
                foreach(var sqlMember in RepresentationToSQLMapping)
                {
                    if (!firstMember)
                    {
                        sbValues.Append(", ");
                    }
                    else
                    {
                        firstMember = false;
                    }
                    Field field = rep[sqlMember.ColumnName];
                    if(field.FieldType == FieldType.String)
                    {
                        sbValues.Append('\'');
                    }
                    sbValues.Append(HttpUtility.JavaScriptStringEncode(field.ValueAsString()));
                    if (field.FieldType == FieldType.String)
                    {
                        sbValues.Append('\'');
                    }
                }
                if(sbValues.Length != 0)
                {
                    sbValues.Append(")");
                }
            }
            string insert = string.Format(insertStatement, TableName, sbColumn.ToString(), sbValues.ToString()); 

            if (truncateTable)
            {
                return string.Format("{0} {1}", TruncateTable(), insert);
            }
            else
            {
                return insert;
            }
        }

        public string TruncateTable()
        {
            return string.Format(truncateStatement, TableName);
        }
    }
}
