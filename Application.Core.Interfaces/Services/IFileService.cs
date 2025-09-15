using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace Application.Core.Interfaces.Services;

public interface IFileService
{
    void SetWindow(Window window);
    
    Task<IReadOnlyList<IStorageFile>> PickFiles();

    Task<IStorageFile?> PickSaveFile();
}