using System;
using System.Collections.Generic;
using System.Text;
using InternalRepresentation;

namespace Export.SQLExport
{
    public class SQLTable
    {
        private List<SQLColumn> RepresentationToSQLMapping;
        private readonly string TableName;

        private const string createStatement = @"CREATE TABLE {0} \n ({1}); ";
        private const string insertStatement = @"INSERT INTO {0} ({1}) VALUES \n {2};";

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

        public string CreateTable()
        {
            StringBuilder sbColumns = new StringBuilder();
            foreach(var sqlMember in RepresentationToSQLMapping)
            {
                if(sbColumns.Length != 0)
                {
                    sbColumns.Append(" ");
                }
                sbColumns.Append(sqlMember.ColumnName);
                sbColumns.Append(" ");
                sbColumns.Append(sqlMember.SQLType);
            }

            return string.Format(createStatement, TableName, sbColumns.ToString());
        }

        public string InsertRepresentationList(RepresentationList repList)
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
                    sbColumn.Append(" ");
                }
                sbColumn.Append(column.ColumnName);
            }

            StringBuilder sbValues = new StringBuilder("(");
            foreach (var rep in repList)
            {
                if(sbValues.Length != 1)
                {
                    sbValues.Append(@",\n(");
                }
                bool firstMember = true;
                foreach(var sqlMember in RepresentationToSQLMapping)
                {
                    if (!firstMember)
                    {
                        sbValues.Append(" ");
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
                    sbValues.Append(field.ValueAsString());
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
            return string.Format(insertStatement, TableName, sbColumn.ToString(), sbValues.ToString());
        }
    }
}
