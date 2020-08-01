using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InternalRepresentation
{
    public class RepresentationList : IEnumerable<Representation>, IEquatable<RepresentationList>
    {
        private List<Representation> _representations;
        private readonly RepresentationTemplate _representationTemplate;

        public RepresentationList(IEnumerable<Field> representation)
        {
            _representationTemplate = new RepresentationTemplate(representation);
            _representations = new List<Representation>();
        }

        public RepresentationList(IEnumerable<string> fieldNames)
        {
            _representationTemplate = new RepresentationTemplate(fieldNames);
            _representations = new List<Representation>();
        }

        public Representation this[int index]
        {
            get
            {
                return _representations[index];
            }

            set
            {
                _representations[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return _representations.Count;
            }
        }

        public Representation CreateNewRepresentation()
        {
            _representations.Add(new Representation(_representationTemplate));
            return _representations.Last();
        }

        public IEnumerator<Representation> GetEnumerator()
        {
            return _representations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _representations.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RepresentationList);
        }

        public bool Equals(RepresentationList other)
        {
            return other != null &&
                   EqualityComparer<List<Representation>>.Default.Equals(_representations, other._representations) &&
                   EqualityComparer<RepresentationTemplate>.Default.Equals(_representationTemplate, other._representationTemplate) &&
                   Count == other.Count;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_representations, _representationTemplate, Count);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var representation in _representations)
            {
                sb.Append(representation.ToString());
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
