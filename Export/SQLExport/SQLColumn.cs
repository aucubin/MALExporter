using System;
using System.Collections.Generic;
using InternalRepresentation;

namespace Export.SQLExport
{
    internal class SQLColumn
    {
        internal readonly string SQLType;
        internal readonly string ColumnName;

        internal SQLColumn(Field field, string columnName)
        {
            SQLType = GetSQLTypeFromField(field);
            ColumnName = columnName;
        }

        private static string GetSQLTypeFromField(Field field)
        {
            Dictionary<FieldType, string> fieldToStringMap = new Dictionary<FieldType, string>()
            {
                { FieldType.Boolean, "boolean" },
                { FieldType.Integer, "integer" },
                { FieldType.String, "text" }
            };

            try
            {
                return fieldToStringMap[field.FieldType];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("Invalid Field");
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentException("Field was null");
            }
        }
    }
}
