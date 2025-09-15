using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.MangaDownload.Interfaces;
using Module.MangaDownload.ViewModels.Components;

namespace Module.MangaDownload.ViewModels;

internal class DesignTimeMangaDownloadViewModel : MangaDownloadViewModel
{
    public DesignTimeMangaDownloadViewModel() : base(null, null, null) { }
}

public partial class MangaDownloadViewModel : ObservableObject
{
    private readonly IMangaPdfService _mangaPdfService;
    
    public MangaSearchViewModel SearchViewModel { get; }
        
    [ObservableProperty] 
    private int _firstChapter = 1;
    [ObservableProperty] 
    private int _lastChapter = 1;
    [ObservableProperty] 
    private bool _canDownload = true;

    partial void OnFirstChapterChanged(int value)
    {
        if (value <= LastChapter)
            return;
        
        LastChapter = value;    
    }

    partial void OnLastChapterChanged(int value)
    {
        if (value >= FirstChapter)
            return;
        FirstChapter = value;
    }
    
    public MangaDownloadViewModel(
        ICatalogService catalogService, 
        IMangaPdfService mangaPdfService,
        IOutputService outputService)
    {
        _mangaPdfService = mangaPdfService;

        SearchViewModel = new MangaSearchViewModel(catalogService, outputService);
    }
    
    [RelayCommand]
    private async Task Process()
    {
        CanDownload = false;

        var processOk = true;
        var currentChapter = FirstChapter;
        while (processOk && currentChapter <= LastChapter)
        {
            processOk = await _mangaPdfService.DownloadChapter(
                SearchViewModel.MangaName, 
                currentChapter);
            currentChapter++;
        }
        
        CanDownload = true;
    }
}