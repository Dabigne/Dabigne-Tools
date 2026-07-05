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
    
    public MangaSearchViewModel MangaSearch { get; }
        
    public ChaptersSelectionViewModel  ChaptersSelection { get; } = new();
    
    [ObservableProperty]
    private int? _numberOfPagesInImage = null;
    
    [ObservableProperty] 
    private bool _canDownload = true;
    
    public MangaDownloadViewModel(
        ICatalogService? catalogService, 
        IMangaPdfService? mangaPdfService,
        IOutputService? outputService)
    {
        _mangaPdfService = mangaPdfService!;

        MangaSearch = new MangaSearchViewModel(catalogService!, outputService!);
    }
    
    [RelayCommand]
    private async Task Process()
    {
        CanDownload = false;

        var processOk = true;
        var currentChapter = ChaptersSelection.FirstChapter;
        while (processOk && currentChapter <= ChaptersSelection.LastChapter)
        {
            processOk = await _mangaPdfService.DownloadChapterToPdf(
                MangaSearch.MangaName, 
                currentChapter,
                NumberOfPagesInImage);
            currentChapter++;
        }
        
        CanDownload = true;
    }
}