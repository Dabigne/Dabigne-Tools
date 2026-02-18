using Application.Core.Interfaces.Services;
using Application.Core.Models;
using SkiaSharp;

namespace Application.Core.Services;

public class ImageDownloaderService : IImageDownloaderService
{
   private readonly IOutputService _outputService;
   private HttpClient? _httpClient;

   public ImageDownloaderService(IOutputService outputService)
   {
      _outputService = outputService;
   }
   
   public void Start()
   {
      var handler = new HttpClientHandler();
      handler.ServerCertificateCustomValidationCallback = 
         (httpRequestMessage, cert, cetChain, policyErrors) =>
         {
            return true;
         };
      
      _httpClient ??= new HttpClient(handler);
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
         _outputService.Push(new OutputLine($"Getting image from {uri}"));
         var imageBytes = await _httpClient.GetByteArrayAsync(uri);
         
         var image = SKImage.FromEncodedData(imageBytes);
         var bitmap = SKBitmap.FromImage(image);
         var data = bitmap.Encode(SKEncodedImageFormat.Png, 100);
         var stream = File.Create(path);
         data.SaveTo(stream);
         stream.Close();
         //await File.WriteAllBytesAsync(path, imageBytes);
         _outputService.Push(new OutputLine($"File saved to {path}"));
      }
      catch (HttpRequestException e)
      {
         _outputService.Push(new OutputLine($"Failed to download file {uri}"));
         _outputService.Push(new OutputLine($"Error: {e.Message}"));
         return false;
      }
      
      return true;
   }
}