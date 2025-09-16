using System.Collections.ObjectModel;
using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Module.PdfTools.ViewModels.Components;

public class FileListViewModelDesign : FileListViewModel
{
    public FileListViewModelDesign() : base(null) {}
}

public partial class FileListViewModel : ObservableObject
{
    private readonly IFileService _fileService;
    
    public ObservableCollection<string> Files { get; } = [];
    
    [ObservableProperty]
    private string? _selectedFile;

    public FileListViewModel(IFileService  fileService)
    {
        _fileService = fileService;
    }
    
    [RelayCommand]
    private async Task PickFiles()
    {
        var files = await _fileService.PickFiles();
        foreach (var storageFile in files)
        {
            Files.Add(storageFile.Path.AbsolutePath);
        }
    }

    [RelayCommand]
    private void ClearFiles()
    {
        Files.Clear();
    }
    
    [RelayCommand]
    private void RemoveFile()
    {
        if (SelectedFile == null)
            return;

        var index = Files.IndexOf(SelectedFile);
        Files.Remove(SelectedFile);
        
        if (Files.Count == 0)
            return;
        
        SelectedFile = index <= Files.Count - 1 
            ? Files[index] 
            : Files[Files.Count - 1];
    }

    [RelayCommand]
    private void MoveUp()
    {
        if (SelectedFile == null)
            return;

        var index = Files.IndexOf(SelectedFile);
        if (index == 0)
            return;

        var file = SelectedFile;
        Files.Remove(SelectedFile);
        Files.Insert(index - 1, file);
        SelectedFile = Files[index - 1];
    }
    
    [RelayCommand]
    private void MoveDown()
    {
        if (SelectedFile == null)
            return;

        var index = Files.IndexOf(SelectedFile);
        if (index == Files.Count - 1)
            return;

        var file = SelectedFile;
        Files.Remove(SelectedFile);
        Files.Insert(index + 1, file);
        SelectedFile = Files[index + 1];
    }
}