using Application.Core.Interfaces.Services;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace Application.Core.Services;

public class FileService : IFileService
{
    private Window _window;
    
    public void SetWindow(Window window)
    {
        _window = window;
    }

    public async Task<IReadOnlyList<IStorageFile>> PickFiles()
    {
        var files = await _window.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select files",
            AllowMultiple = true
        });

        var test = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        return files;
    }

    public async Task<IStorageFile?> PickSaveFile()
    {
        var file = await _window.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Select file to save",
        });
        
        return file;
    }
}