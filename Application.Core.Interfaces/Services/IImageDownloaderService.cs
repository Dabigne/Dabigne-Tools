namespace Application.Core.Interfaces.Services;

public interface IImageDownloaderService
{
    void Start();
    void Stop();
    Task<bool> GetImage(string directoryPath, string fileName, Uri uri);
}