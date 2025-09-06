using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.MangaDownload.Services;

namespace Module.MangaDownload.ViewModels;

internal class DesignTimeMangaDownloadViewModel : MangaDownloadViewModel
{
    public DesignTimeMangaDownloadViewModel() : base(null) { }
}

public partial class MangaDownloadViewModel : ObservableObject
{
    private readonly MangaPdfService _mangaPdfService;
    
    [ObservableProperty] 
    private string _mangaName = "Mashle";
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
    
    public MangaDownloadViewModel(MangaPdfService mangaPdfService)
    {
        _mangaPdfService = mangaPdfService;
    }
    
    [RelayCommand]
    private async Task Process()
    {
        CanDownload = false;

        var processOk = true;
        var currentChapter = FirstChapter;
        while (processOk && currentChapter <= LastChapter)
        {
            processOk = await _mangaPdfService.DownloadChapter(MangaName, currentChapter);
            currentChapter++;
        }
        
        CanDownload = true;
    }
}