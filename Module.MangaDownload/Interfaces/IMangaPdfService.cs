namespace Module.MangaDownload.Interfaces;

public interface IMangaPdfService
{
    Task<bool> DownloadChapterToPdf(string mangaName, int chapterToDownload);
}