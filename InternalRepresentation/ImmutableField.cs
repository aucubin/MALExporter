using System;

namespace InternalRepresentation
{
    public class ImmutableField : Field
    {
        public ImmutableField(string fieldName) : base(fieldName) { }

        public ImmutableField(string fieldName, FieldType fieldType) : base(fieldName, fieldType) { }

        public ImmutableField(Field field) : base(field) { }

        public new bool BoolValue
        {
            get;
        }

        public new int IntValue
        {
            get;
        }

        public new string StringValue
        {
            get;
        }
    }
}
