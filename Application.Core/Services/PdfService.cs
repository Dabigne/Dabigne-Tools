using Application.Core.Interfaces.Services;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Application.Core.Services;

public class PdfService : IPdfService
{
    public bool CreatePdfFromImagesInFolder(string folderName, string pdfName)
    {
        var orderedList = Directory.GetFiles(folderName).ToList().Order();
        var finalList = orderedList.Where(s => s.EndsWith(".jpg")).ToList();
        
        if (!finalList.Any())
            return false;

        using var document = new PdfDocument();
        foreach (var pageName in finalList)
        {
            var page = document.AddPage();
            using var img = XImage.FromFile(pageName);
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
        
        return true;
    }
}