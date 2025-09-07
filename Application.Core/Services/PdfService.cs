using System.Drawing;
using System.Drawing.Imaging;
using Application.Core.Interfaces.Services;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SkiaSharp;

namespace Application.Core.Services;

public class PdfService : IPdfService
{
    private readonly IOutputService _outputService;

    public PdfService(IOutputService outputService)
    {
        _outputService = outputService;
    }
    
    public bool CreatePdfFromImagesInFolder(string folderName, string pdfName)
    {
        var orderedList = Directory.GetFiles(folderName).ToList().Order();
        var finalList = orderedList.Where(s => s.EndsWith(".jpg")).ToList();
        
        if (!finalList.Any())
            return false;

        try
        {
            using var document = new PdfDocument();
            foreach (var pageName in finalList)
            {
                _outputService.Push($"Adding page {pageName}");
                var page = document.AddPage();
                using var img = GetXImageFromPath(pageName);
                var pageWidth = img.PixelWidth;
                var pageHeight = img.PixelHeight;
                    
                // Change PDF Page size to match image
                page.Width = new XUnit(pageWidth);
                page.Height = new XUnit(pageHeight);

                var gfx = XGraphics.FromPdfPage(page);
                gfx.DrawImage(img, 0, 0, pageWidth, pageHeight);
            }
            var pdfPath = Path.Join(folderName, pdfName);
            document.Save($"{pdfPath}.pdf");
            _outputService.Push($"Pdf generated {pdfPath}.pdf");
        }
        catch (Exception e)
        {
            _outputService.Push($"Error creating PDF: {e.Message}");
        }
        
        return true;
    }

    private XImage GetXImageFromPath(string path)
    {
        try
        {
            var fileStream = File.OpenRead(path);
            return XImage.FromStream(fileStream);
        }
        catch (Exception)
        {
            _outputService.Push($"Converting image to supported format: {path}");
            var image = SKImage.FromEncodedData(path);
            var data = image.Encode(SKEncodedImageFormat.Png, 100);
            return XImage.FromStream(data.AsStream());
        }
    }
}