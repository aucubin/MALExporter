using Xunit;
using InternalRepresentation;
using System;

namespace UnitTests.InternalRepresentation
{
    public class FieldTests
    {
        
        [Fact]
        public void NewFieldIsInvalid()
        {
            Field newField = new Field("");
            Assert.Equal(FieldType.Invalid, newField.FieldType);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void BoolFieldIsBoolType(bool b)
        {
            Field newField = new Field("")
            {
                BoolValue = b
            };
            Assert.Equal(FieldType.Boolean, newField.FieldType);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        public void IntFieldIsIntType(int i)
        {
            Field newField = new Field("")
            {
                IntValue = i
            };
            Assert.Equal(FieldType.Integer, newField.FieldType);
        }

        [Theory]
        [InlineData("")]
        [InlineData("test")]
        public void StringFieldIsStringType(string s)
        {
            Field newField = new Field("")
            {
                StringValue = s
            };
            Assert.Equal(FieldType.String, newField.FieldType);
        }

        [Theory]
        [InlineData(true,1)]
        [InlineData(1,true)]
        [InlineData("test",1)]
        [InlineData(1,"test")]
        public void FieldTypeChangesOnValueTypeChange(object a, object b)
        {
            FieldType oldFieldType = GetFieldType(a);
            FieldType newFieldType = GetFieldType(b);

            Assert.NotEqual(oldFieldType, newFieldType);

            Field newField = new Field("");
            switch (oldFieldType)
            {
                case FieldType.String:
                    newField.StringValue = (string)a;
                    break;
                case FieldType.Integer:
                    newField.IntValue = (int)a;
                    break;
                case FieldType.Boolean:
                    newField.BoolValue = (bool)a;
                    break;
            }

            Assert.Equal(oldFieldType, newField.FieldType);

            switch (newFieldType)
            {
                case FieldType.String:
                    newField.StringValue = (string)b;
                    break;
                case FieldType.Integer:
                    newField.IntValue = (int)b;
                    break;
                case FieldType.Boolean:
                    newField.BoolValue = (bool)b;
                    break;
            }

            Assert.Equal(newFieldType, newField.FieldType);
        }

        private FieldType GetFieldType(object o)
        {
            Type CurrentType = o.GetType();
            
            if(CurrentType == typeof(string))
            {
                return FieldType.String;
            }
            else if(CurrentType == typeof(int))
            {
                return FieldType.Integer;
            }
            else if(CurrentType == typeof(bool))
            {
                return FieldType.Boolean;
            }
            else
            {
                return FieldType.Invalid;
            }
        }
    }
}
