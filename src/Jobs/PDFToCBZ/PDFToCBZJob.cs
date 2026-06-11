using Microsoft.Extensions.Logging;
using PDFToCBZ.Services;

namespace PDFToCBZ
{
    public class PDFToCBZJob
    {
        private readonly PDFToImagesService _pdfToImagesService;
        private readonly ZipService _zipService;
        private readonly ILogger<PDFToCBZJob> _logger;
        private readonly string _inputFolder;
        private readonly string _workFolder;
        private readonly string _outputFolder;

        public PDFToCBZJob(
            PDFToImagesService pdfToImagesService,
            ZipService zipService,
            ILogger<PDFToCBZJob> logger,
            string inputFolder,
            string workFolder,
            string outputFolder)
        {
            _pdfToImagesService = pdfToImagesService;
            _zipService = zipService;
            _logger = logger;
            _inputFolder = inputFolder;
            _workFolder = workFolder;
            _outputFolder = outputFolder;
        }

        public async Task RunAsync()
        {
            _logger.LogInformation("Environment InputFolder: {InputFolder}", _inputFolder);
            _logger.LogInformation("Environment WorkFolder: {WorkFolder}", _workFolder);
            _logger.LogInformation("Environment OutputFolder: {OutputFolder}", _outputFolder);

            if (Directory.Exists(_workFolder) && Directory.GetFiles(_workFolder).Any())
            {
                _logger.LogError("Work folder is not empty: {WorkFolder}", _workFolder);
                return;
            }

            if (!ValidateOrCreateWorkFolder(_workFolder))
            {
                _logger.LogError("Failed to create work folder: {WorkFolder}", _workFolder);
                return;
            }

            string[] files = Directory.GetFiles(_inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
            _logger.LogInformation("Converting {FileCount} files", files.Length);

            foreach (var file in files)
            {
                _logger.LogInformation("Processing file: {File}", file);

                var filename = Path.GetFileName(file);
                var workFileWithPath = Path.Combine(_workFolder, filename);
                File.Copy(file, workFileWithPath);

                _logger.LogInformation("Extracting images...");
                var pdfBytes = await File.ReadAllBytesAsync(workFileWithPath);
                var bitmaps = _pdfToImagesService.ConvertPdfByteStreamToImages(pdfBytes);

                _logger.LogInformation("Converting to JPG...");
                var jpgs = _pdfToImagesService.ConvertBitmapsToJpgs(bitmaps);

                _logger.LogInformation("Writing JPGs to files...");
                var jpgFileNames = WriteJpgsToFiles(_workFolder, jpgs);

                _logger.LogInformation("Creating CBZ file...");
                var cbzFileName = Path.Combine(
                    Path.GetFullPath(_outputFolder),
                    Path.GetFileNameWithoutExtension(workFileWithPath) + ".cbz");

                _zipService.CreateZipFile(cbzFileName, jpgFileNames);

                _logger.LogInformation("Cleaning up work folder...");
                DeleteWorkFiles(_workFolder);
            }

            _logger.LogInformation("Done.");
        }

        private List<string> WriteJpgsToFiles(string workFolder, List<byte[]> jpgs)
        {
            var jpgFileNames = new List<string>();
            var fileNumber = 1;

            foreach (var jpg in jpgs)
            {
                var fileName = fileNumber.ToString("D3") + ".jpg";
                var fileNameWithPath = Path.Combine(workFolder, fileName);
                File.WriteAllBytes(fileNameWithPath, jpg);
                jpgFileNames.Add(fileNameWithPath);
                fileNumber++;
            }

            return jpgFileNames;
        }

        private bool ValidateOrCreateWorkFolder(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    DeleteWorkFiles(path);
                    return true;
                }

                var dirInfo = Directory.CreateDirectory(path);
                return dirInfo != null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create work folder: {Error}", ex.Message);
                return false;
            }
        }

        private void DeleteWorkFiles(string folderPath)
        {
            foreach (var file in Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly))
            {
                File.Delete(file);
            }
        }
    }
}