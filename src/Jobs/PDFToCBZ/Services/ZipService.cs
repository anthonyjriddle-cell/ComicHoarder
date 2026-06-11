using System.IO.Compression;

namespace PDFToCBZ.Services
{
    public class ZipService
    {
        public string CreateZipFile(string zipfilename, List<string> filenames)
        {
            if (filenames.Any())
            {
                string? workPath = Path.GetDirectoryName(filenames.FirstOrDefault()) ?? "workfolder";
                ZipFile.CreateFromDirectory(workPath, zipfilename);
                return Path.Combine(workPath, zipfilename);
            }
            return "";
        }
    }
}
