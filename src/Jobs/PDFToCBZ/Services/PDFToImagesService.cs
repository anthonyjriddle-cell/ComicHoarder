using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace PDFToCBZ.Services
{
    public class PDFToImagesService
    {

        public List<SKBitmap> ConvertPdfByteStreamToImages(byte[] pdfAsByteArray)
        {
            return PDFtoImage.Conversion.ToImages(pdfAsByteArray).ToList();
        }

        public List<byte[]> ConvertBitmapsToJpgs(List<SKBitmap> skBitmaps)
        {
            List<byte[]> jpgImages = new List<byte[]>();

            foreach (var skBitmap in skBitmaps)
            {
                // Convert SKBitmap to JPEG byte array
                using (MemoryStream stream = new MemoryStream())
                {
                    skBitmap.Encode(SKEncodedImageFormat.Jpeg, 25).SaveTo(stream);
                    jpgImages.Add(stream.ToArray());
                }
            }

            return jpgImages;
        }
    }
}
