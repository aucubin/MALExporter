using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace InternalRepresentation
{
    internal class RepresentationFieldEqualityComparer : EqualityComparer<Dictionary<string, Field>>
    {
        public override bool Equals([AllowNull] Dictionary<string, Field> x, [AllowNull] Dictionary<string, Field> y)
        {
            if(x == null || y == null)
            {
                return false;
            }

            if(x.Count != y.Count)
            {
                return false;
            }

            foreach(KeyValuePair<string,Field> pair in x)
            {
                if (!pair.Value.Equals(y[pair.Key])){
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode([DisallowNull] Dictionary<string, Field> obj)
        {
            HashCode hashCode = new HashCode();
            foreach(KeyValuePair<string,Field> pair in obj)
            {
                hashCode.Add(pair.Value.GetHashCode());
            }
            return hashCode.ToHashCode();
        }

        public static RepresentationFieldEqualityComparer EqualityComparer{
            get
            {
                return new RepresentationFieldEqualityComparer();
            }
        }
    }
}
