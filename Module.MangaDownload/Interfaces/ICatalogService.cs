namespace Module.MangaDownload.Interfaces;

public interface ICatalogService
{
    Task<IEnumerable<string>> Search(string query);
}