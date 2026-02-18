using System.Collections.ObjectModel;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Application.Core.Interfaces.Services;
using Application.Core.Models;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Module.WorkShopTools.ViewModels;

public partial class ModeExplorerViewModel : ObservableObject
{
    private readonly IFileService  _fileService;
    private readonly IImageDownloaderService  _imageDownloaderService;
    private readonly IOutputService _outputService;

    public ObservableCollection<string> Urls { get; } = [];
    
    [ObservableProperty]
    private string selectedUrl;

    [ObservableProperty] 
    private bool _removeLastSlash;
    
    [ObservableProperty]
    private int _urlLength = 105;

    public ModeExplorerViewModel(
        IFileService fileService,
        IImageDownloaderService imageDownloaderService,
        IOutputService outputService)
    {
        _fileService = fileService;
        _imageDownloaderService = imageDownloaderService;
        _outputService = outputService;
    }
    
    [RelayCommand]
    private async Task LoadFile()
    {
        var file = await _fileService.PickLoadFile();
        if (file is null)
            return;
        
        Urls.Clear();
        
        var text = File.ReadAllText(file.Path.LocalPath);
        
        var indexes = new List<int>();
        int index = 0;
        while ((index = text.IndexOf("https", index, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            indexes.Add(index++);
        }

        foreach (var entry in indexes)
        {
            var url = text.Substring(entry, UrlLength);
            if (RemoveLastSlash)
            {
                var lastSlash = url.LastIndexOf("/") + 1;
                url = url.Substring(0, lastSlash);
            }
            if (Urls.Contains(url))
                continue;
            Urls.Add(url);
        }
    
        _outputService.Push(new OutputLine($"File count: {Urls.Count}"));
    }

    [RelayCommand]
    private async Task Download()
    {
        if (Urls.Count == 0)
            return;

        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        folderPath = Path.Combine(folderPath, "PrimalAssets");
        folderPath = Path.Combine(folderPath, "Images");

        _imageDownloaderService.Start();
        var imageIndex = 0;
        foreach (var url in Urls)
        {
            try
            {
                _outputService.Push(new OutputLine($"Dowloading: {url}"));
                await _imageDownloaderService.GetImage(folderPath, $"Image{imageIndex++}.png", new Uri(url));
            }
            catch (Exception e)
            {
                _outputService.Push(
                    new OutputLine($"Failed to dowload: {url}", false, Color.FromRgb(255,0,0)));
            }
        }
        _imageDownloaderService.Stop();
    }

    partial void OnSelectedUrlChanged(string value)
    {
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime
            {
                MainWindow: { } window
            })
        {
            var clipboard =  window.Clipboard!;
            clipboard.SetTextAsync(value);
        }
    }
}