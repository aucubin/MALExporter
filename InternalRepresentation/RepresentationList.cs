﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InternalRepresentation
{
    public class RepresentationList : IEnumerable<Representation>
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
    }
}
