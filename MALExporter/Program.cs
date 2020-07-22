using InternalRepresentation;
using System;
using System.Collections.Generic;

namespace MALExporter
{
    class Program
    {
        static void Main()
        {
            var representation = new Representation(new List<string>()
            {
                "test", "test2"
            });

            representation["test"].StringValue = "test";
            representation["test2"].StringValue = "test2";

            foreach(var field in representation)
            {
                Console.WriteLine("Key: "+field.Key+", Value: "+field.Value);
            }

            Console.WriteLine();

            var representationSet = new RepresentationList(new List<string>()
            {
                "test1",
                "test2",
                "test3"
            });

            var representation1 = representationSet.CreateNewRepresentation();
            representation1["test1"].StringValue = "test2";
            representation1["test3"].IntValue = -10;

            foreach(var field in representation1)
            {
                Console.WriteLine("Key: " + field.Key + ", Value: " + field.Value);
            }

            representation1["test1"] = representation1["test3"];
        }
    }
}
