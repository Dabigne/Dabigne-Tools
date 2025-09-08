namespace Module.MangaDownload.Interfaces;

public interface IMangaPdfService
{
    Task<bool> DownloadChapter(string mangaName, int chapterToDownload);
}