using Xunit;
using InternalRepresentation;
using System.Collections.Generic;

namespace UnitTests.InternalRepresentation
{
    public class RepresentationListTests
    {
        [Fact]
        public void EmptyRepresentationListIsCreated()
        {

            RepresentationList r = new RepresentationList(CreateTestFieldList());

            Assert.Equal(0, r.Count);
        }

        [Fact]
        public void CreateRepresentationFromRepList()
        {
            RepresentationList r = new RepresentationList(CreateTestFieldList());
            r.CreateNewRepresentation();
            Assert.Equal(1, r.Count);

            Representation testRep = new Representation(CreateTestFieldList());

            Assert.True(r[0].Equals(testRep));
        }

        private List<string> CreateTestFieldList()
        {
            return new List<string>()
            {
                "field1",
                "field2",
                "field3"
            };
        }
    }
}
