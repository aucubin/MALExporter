using InternalRepresentation;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Export.CSVExport
{
    public class CSVMain
    {
        private readonly string FilePath;
        private readonly bool OverwriteFile;
        private readonly RepresentationList Representations;

        // used to get a order in which the fields are appended to the csv file as fields
        // in one representation can potentially be sorted in another order than in another representation of the same representationlist
        // (dictionary class from C# does not specify order in which its keys are ordered)
        private readonly List<string> fieldOrdering;

        public CSVMain(string filePath, bool overwriteFile, RepresentationList representations)
        {
            FilePath = filePath;
            OverwriteFile = overwriteFile;
            Representations = representations;
            fieldOrdering = new List<string>();
        }

        public void Export()
        {
            CheckFileExists();
            WriteRepresentationToFile();
        }

        private void CheckFileExists()
        {
            if (File.Exists(FilePath) && !OverwriteFile)
            {
                throw new IOException("File to write into already exists and no --force option was given");
            }
        }

        private void WriteRepresentationToFile()
        {
            using(StreamWriter sw = File.CreateText(FilePath))
            {
                sw.WriteLine(CreateCSVHeader());
                foreach(var rep in Representations)
                {
                    sw.WriteLine(CreateCSVRow(rep));
                }
            }
        }

        private string CreateCSVHeader()
        {
            StringBuilder sb = new StringBuilder();
            Representation firstRepresentation = Representations[0];
            foreach (var pair in firstRepresentation)
            {
                if(sb.Length != 0)
                {
                    sb.Append(',');
                }
                sb.Append(pair.Key);
                fieldOrdering.Add(pair.Key);
            }
            return sb.ToString();
        }

        private string CreateCSVRow(Representation rep)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var fieldOrder in fieldOrdering)
            {
                var field = rep[fieldOrder];
                if(sb.Length != 0)
                {
                    sb.Append(',');
                }

                if(field.FieldType != FieldType.Invalid)
                {
                    sb.Append(field.ValueAsString());
                }
            }
            return sb.ToString();
        }
    }
}
