using System;
using System.IO;

namespace ComicHoarder.Jobs.Tests.TestUtilities
{
    public static class TestFileLoader
    {
        /// <summary>
        /// Loads a test file from the TestFiles folder in the test project.
        /// </summary>
        /// <param name="relativePath">Path relative to the TestFiles folder.</param>
        public static string Load(string relativePath)
        {
            var baseDir = AppContext.BaseDirectory;

            var fullPath = Path.Combine(
                baseDir,
                "TestFiles",
                relativePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException(
                    $"Test file not found: {fullPath}");

            return File.ReadAllText(fullPath);
        }
    }
}