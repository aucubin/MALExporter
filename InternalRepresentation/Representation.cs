using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace InternalRepresentation
{
    public class Representation : IEnumerable<KeyValuePair<string,Field>>
    {
        private Dictionary<string,Field> _fields;

        public Representation(IEnumerable<Field> fields)
        {
            _fields = new Dictionary<string, Field>();
            foreach(Field field in fields)
            {
                _fields.Add(field.FieldName, field);
            }
        }

        public Representation(IEnumerable<string> fieldNames)
        {
            _fields = new Dictionary<string, Field>();
            foreach(string fieldName in fieldNames)
            {
                _fields.Add(fieldName,new Field(fieldName));
            }
        }

        internal Representation(RepresentationTemplate template)
        {
            _fields = new Dictionary<string, Field>();
            foreach(ImmutableField field in template.Fields)
            {
                _fields.Add(field.FieldName, (ImmutableField)field.Clone());
            }
        }

        public Field this[string name]
        {
            get
            {
                return _fields[name];
            }
            set
            {
                if(value.FieldName != name)
                {
                    throw new ArgumentException("Reassigning of Field is not allowed if new Field has another Field Name than old Field.");
                }
                _fields[name] = value;
            }
        }

        public int Count
        {
            get
            {
                return _fields.Count;
            }
        }

        public IEnumerator<KeyValuePair<string,Field>> GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var field in _fields)
            {
                sb.Append(field.Value.ToString());
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
