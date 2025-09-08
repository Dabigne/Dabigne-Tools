using Application.Core.Interfaces.Services;
using Module.MangaDownload.Interfaces;

namespace Module.MangaDownload.Services;

public class MangaPdfService : IMangaPdfService
{
    private const string UrlRoot = "https://anime-sama.fr/s2/scans/";
    private const string MangasRoot = "Mangas";

    private readonly IImageDownloaderService  _imageDownloaderService;
    private readonly IPdfService _pdfService;

    public MangaPdfService(
        IImageDownloaderService imageDownloaderService, 
        IPdfService pdfService)
    {
        _imageDownloaderService = imageDownloaderService;
        _pdfService = pdfService;
    }
    
    public async Task<bool> DownloadChapter(string mangaName, int chapterToDownload)
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var chapterString = chapterToDownload.ToString("D3");
        folderPath = Path.Combine(folderPath, MangasRoot);
        folderPath = Path.Combine(folderPath, mangaName);
        folderPath = Path.Combine(folderPath, chapterString);

        var page = 1;
        var result = true;
        
        _imageDownloaderService.Start();
        while (result == true)
        {
            var imageUri = new Uri($"{UrlRoot}{mangaName}/{chapterToDownload}/{page}.jpg");
            result = await _imageDownloaderService.GetImage(folderPath, page.ToString("D3"), imageUri);
            page++;
        }
        _imageDownloaderService.Stop();
        
        return _pdfService.CreatePdfFromImagesInFolder(folderPath, $"{mangaName}{chapterString}");
    }
}