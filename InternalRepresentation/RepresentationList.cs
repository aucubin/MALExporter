using System.Collections;
using System.Collections.Generic;

namespace InternalRepresentation
{
    public class RepresentationList : IEnumerable<Representation>
    {
        private List<Representation> _representations;
        private readonly List<Field>_representationTemplate;

        public RepresentationList(List<Field> representation)
        {
            _representationTemplate = representation;
            _representations = new List<Representation>();
        }

        public RepresentationList(List<string> fieldNames)
        {
            _representationTemplate = new List<Field>();
            _representations = new List<Representation>();
            foreach(string fieldName in fieldNames)
            {
                _representationTemplate.Add(new Field(fieldName));
            }
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
            return _representations[^1];
        }

        public IEnumerator<Representation> GetEnumerator()
        {
            return _representations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _representations.GetEnumerator();
        }
    }
}
