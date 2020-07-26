using System;
using System.Collections.Generic;

namespace InternalRepresentation
{
    internal class RepresentationTemplate : IEquatable<RepresentationTemplate>
    {
        private readonly List<ImmutableField> _fields;

        internal ICollection<ImmutableField> Fields
        {
            get
            {
                return _fields.AsReadOnly();
            }
        }

        internal RepresentationTemplate(IEnumerable<string> fieldNames)
        {
            _fields = new List<ImmutableField>();
            foreach(string fieldName in fieldNames)
            {
                _fields.Add(new ImmutableField(fieldName));
            }
        } 

        internal RepresentationTemplate(IEnumerable<Field> fields)
        {
            _fields = new List<ImmutableField>();
            foreach(Field field in fields)
            {
                _fields.Add(new ImmutableField(field));
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RepresentationTemplate);
        }

        public bool Equals(RepresentationTemplate other)
        {
            return other != null &&
                   EqualityComparer<List<ImmutableField>>.Default.Equals(_fields, other._fields);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_fields);
        }
    }
}
