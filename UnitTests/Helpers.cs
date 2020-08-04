using System.IO;
using System.Reflection;

namespace UnitTests
{
    internal class Helpers
    {
        internal static string GetAbsolutePathForExternalUnitTestingFile(string fileName)
        {
            return Path.Combine(GetPathToCurrentUnitTestingProject(), fileName);
        }

        private static string GetPathToCurrentUnitTestingProject()
        {
            string pathAssembly = Assembly.GetExecutingAssembly().Location;
            string folderAssembly = Path.GetDirectoryName(pathAssembly);
            if (!folderAssembly.EndsWith(Path.DirectorySeparatorChar))
            {
                folderAssembly += Path.DirectorySeparatorChar;
            }
            return Path.GetFullPath(folderAssembly + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar);
        }
    }
}
