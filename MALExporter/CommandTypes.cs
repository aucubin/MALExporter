using System;
using System.Collections.Generic;

namespace MALExporter
{
    public class CommaSeperatedList
    {
        private readonly List<string> internalList;
        public IReadOnlyCollection<string> ParsedList
        {
            get
            {
                return internalList.AsReadOnly();
            }
        }

        public CommaSeperatedList(string input)
        {
            if(input == string.Empty)
            {
                throw new ArgumentException("Empty argument found");
            }

            internalList = new List<string>(input.Split(',',StringSplitOptions.RemoveEmptyEntries));
        }
    }

    public class CommaSeperatedListWithAssignment
    {
        private readonly List<Tuple<string, string>> internalMap;
        public IEnumerable<Tuple<string,string>> ParsedList
        {
            get
            {
                return internalMap;
            }
        }

        public CommaSeperatedListWithAssignment(string input)
        {
            CommaSeperatedList tmpList = new CommaSeperatedList(input);
            internalMap = new List<Tuple<string, string>>();
            foreach(string arg in tmpList.ParsedList)
            {
                string[] tmpString = arg.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if(tmpString.Length == 1)
                {
                    internalMap.Add(new Tuple<string, string>(tmpString[0], tmpString[0]));
                }
                else if(tmpString.Length > 2)
                {
                    throw new ArgumentException("Argument with too little or too many assignments found");
                }
                else
                {
                    internalMap.Add(new Tuple<string, string>(tmpString[0], tmpString[1]));
                }
            }
        }
    }
}
