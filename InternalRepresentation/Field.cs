using System;
using System.Text;

namespace InternalRepresentation
{
    public enum FieldType
    {
        Boolean,
        Integer,
        String,
        Invalid
    }

    public class Field
    {
        public Field(string fieldName) : this(fieldName, FieldType.Invalid) { }

        public Field(string fieldName, FieldType fieldType)
        {
            FieldName = fieldName;
            FieldType = fieldType;
        }

        public Field(Field field) : this(field.FieldName)
        {
            FieldType = field.FieldType;
            switch (FieldType)
            {
                case FieldType.Boolean:
                    BoolValue = field.BoolValue;
                    break;
                case FieldType.Integer:
                    IntValue = field.IntValue;
                    break;
                case FieldType.String:
                    StringValue = field.StringValue;
                    break;
            }
        }

        public readonly string FieldName;

        public FieldType FieldType
        {
            get;
            protected set;
        }

        private bool _boolValue;

        public bool BoolValue
        {
            get {
                if(FieldType != FieldType.Boolean)
                {
                    throw new ArgumentException("FieldType is not Boolean");
                }
                else if(FieldType == FieldType.Invalid)
                {
                    throw new ArgumentException("Field is invalid");
                }
                return _boolValue;
            }

            set
            {
                FieldType = FieldType.Boolean;
                _boolValue = value;
            }
        }

        private int _intValue;

        public int IntValue
        {
            get
            {
                if(FieldType != FieldType.Integer)
                {
                    throw new ArgumentException("FieldType is not Integer");
                }
                else if (FieldType == FieldType.Invalid)
                {
                    throw new ArgumentException("Field is invalid");
                }
                return _intValue;
            }
            set
            {
                FieldType = FieldType.Integer;
                _intValue = value;
            }
        }

        private string _stringValue;

        public string StringValue
        {
            get
            {
                if(FieldType != FieldType.String)
                {
                    throw new ArgumentException("FieldType is not String");
                }
                else if (FieldType == FieldType.Invalid)
                {
                    throw new ArgumentException("Field is invalid");
                }
                return _stringValue;
            }
            set
            {
                FieldType = FieldType.String;
                _stringValue = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("FieldName: " + FieldName + ", ");
            sb.Append("FieldType: " + FieldType + ", ");
            sb.Append("Value: ");
            switch (FieldType)
            {
                case FieldType.Boolean:
                    sb.Append(BoolValue);
                    break;
                case FieldType.Integer:
                    sb.Append(IntValue);
                    break;
                case FieldType.String:
                    sb.Append(StringValue);
                    break;
                default:
                    sb.Append("Invalid");
                    break;
            }
            return sb.ToString();
        }
    }
}
