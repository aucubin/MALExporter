using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace UnitTests
{
    internal static class Helpers
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

        internal static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie)
            {
                action(e, i++);
            }
        }
    }
}
