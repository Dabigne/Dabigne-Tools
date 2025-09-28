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
    
    public bool CreatePdfFromImagesInFolder(string folderName, string pdfName)
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

    private XImage GetXImageFromPath(string path)
    {
        try
        {
            var fileStream = File.OpenRead(path);
            return XImage.FromStream(fileStream);
        }
        catch (Exception)
        {
            _outputService.Push(new OutputLine($"Converting image to supported format: {path}", false, Colors.DarkOrange));
            var image = SKImage.FromEncodedData(path);
            var data = image.Encode(SKEncodedImageFormat.Png, 100);
            return XImage.FromStream(data.AsStream());
        }
    }
}