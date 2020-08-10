using InternalRepresentation;
using System.IO;
using System.Text;

namespace Export.CSVExport
{
    public class CSVExport
    {
        private readonly string FilePath;
        private readonly bool OverwriteFile;
        private readonly RepresentationList Representations;

        public CSVExport(string filePath, bool overwriteFile, RepresentationList representations)
        {
            FilePath = filePath;
            OverwriteFile = overwriteFile;
            Representations = representations;
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
            }
            return sb.ToString();
        }

        private string CreateCSVRow(Representation rep)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var pair in rep)
            {
                if(sb.Length != 0)
                {
                    sb.Append(',');
                }

                if(pair.Value.FieldType != FieldType.Invalid)
                {
                    sb.Append(pair.Value.ValueAsString());
                }
            }
            return sb.ToString();
        }
    }
}
