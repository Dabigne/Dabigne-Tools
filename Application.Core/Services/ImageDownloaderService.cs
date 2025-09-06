using Application.Core.Interfaces.Services;

namespace Application.Core.Services;

public class ImageDownloaderService : IImageDownloaderService
{
   private HttpClient? _httpClient;
   
   public void Start()
   {
      _httpClient ??= new HttpClient();
   }

   public void Stop()
   {
      if (_httpClient == null) 
         return;
      
      _httpClient.Dispose();
      _httpClient = null;
   }
   
   public async Task<bool> GetImage(string directoryPath, string fileName, Uri uri)
   {
      if (_httpClient == null) 
         return false;

      // Get the file extension
      var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
      var fileExtension = Path.GetExtension(uriWithoutQuery);

      // Create file path and ensure directory exists
      var path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
      Directory.CreateDirectory(directoryPath);

      // Download the image and write to the file
      try
      {
         var imageBytes = await _httpClient.GetByteArrayAsync(uri);
         await File.WriteAllBytesAsync(path, imageBytes);
      }
      catch (HttpRequestException)
      {
         return false;
      }
      
      return true;
   }
}