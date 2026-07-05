using Application.Core.Interfaces.Services;
using Application.Core.Models;
using Avalonia.Media;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using SkiaSharp;

namespace Application.Core.Services;

public class PdfService : IPdfService
{
    private readonly IOutputService _outputService;

    public PdfService(IOutputService outputService)
    {
        _outputService = outputService;
    }

    public int GetPageNumberFromFiles(IList<string> pdfPaths)
    {
        if (pdfPaths.Count == 0)
            return 0;
        
        return pdfPaths.Select(pdfPath => PdfReader.Open(pdfPath, PdfDocumentOpenMode.Import))
            .Select(pdfDocument => pdfDocument.PageCount)
            .Sum();
    }
    
    public bool CreatePdfFromImagesInFolder(string folderName, string pdfName, int? maxPageHeight = null)
    {
        var orderedList = Directory.GetFiles(folderName).ToList().Order();
        var finalList = orderedList.Where(s => s.EndsWith(".jpg")).ToList();
        
        if (finalList.Count == 0)
            return false;

        try
        {
            using var document = new PdfDocument();
            
            foreach (var pageName in finalList)
            {
                _outputService.Push(new OutputLine($"Adding page {pageName}"));
                
                using var image = SKImage.FromEncodedData(pageName);
                
                var finalPageHeight = maxPageHeight ?? image.Height;
                if (finalPageHeight > image.Height)
					finalPageHeight = image.Height;
                
                var numberOfPagesInImage = image.Height / finalPageHeight;
                
                for (var i = 0; i < numberOfPagesInImage; i++)
                {
	                var page = document.AddPage();
	                using var img = GetXImageFromPath(image, finalPageHeight, i * finalPageHeight);
	                var pageWidth = img.PixelWidth;
	                var pageHeight = img.PixelHeight;
	                    
	                // Change PDF Page size to match image
	                page.Width = new XUnit(pageWidth);
	                page.Height = new XUnit(pageHeight);

	                var gfx = XGraphics.FromPdfPage(page);
	                gfx.DrawImage(img, 0, 0, pageWidth, pageHeight);
                }
            }
            var pdfPath = Path.Join(folderName, pdfName);
            document.Save($"{pdfPath}.pdf");
            _outputService.Push(new OutputLine($"Pdf generated {pdfPath}.pdf", false, Colors.LimeGreen));
        }
        catch (Exception e)
        {
            _outputService.Push(new OutputLine($"Error creating PDF: {e.Message}", false, Colors.Red));
        }
        
        return true;
    }

    public bool MergePdfsIntoOne(List<string> pdfPaths, string pdfName)
    {
        if (pdfPaths.Count == 0)
            return false;
        
        try
        {
            using var document = new PdfDocument();
            foreach (var path in pdfPaths)
            {
                _outputService.Push(new OutputLine($"Adding pdf {path}"));
                var pdf = PdfReader.Open(path, PdfDocumentOpenMode.Import);
                foreach (var pdfPage in pdf.Pages)
                {
                    document.AddPage(pdfPage);
                }
            }
            document.Save(pdfName);
        }
        catch (Exception e)
        {
            _outputService.Push(new OutputLine($"Error merging PDF: {e.Message}", false, Colors.Red));
            return false;
        }
        
        return true;
    }

    private XImage GetXImageFromPath(SKImage image, int height, int? heightStart = null)
    {
        using var skBitmap = SKBitmap.Decode(image.EncodedData);
        using var pixmap =  new SKPixmap(skBitmap.Info, skBitmap.GetPixels());
        var top = heightStart ?? 0;
        SKRectI rectI = new SKRectI(0, 
	        top, 
	        image.Width, 
	        top + height);
        var subset = pixmap.ExtractSubset(rectI);
        
        using var data = subset.Encode(SKEncodedImageFormat.Png, 100);
        return XImage.FromStream(data.AsStream());
    }
}