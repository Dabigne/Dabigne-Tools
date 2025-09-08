using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.MangaDownload.Interfaces;

namespace Module.MangaDownload.ViewModels.Components;

public partial class MangaSearchViewModel : ObservableObject
{
    private readonly ICatalogService _catalogService;

    [ObservableProperty] 
    private string _mangaName = string.Empty;

    public MangaSearchViewModel(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }
    
    [RelayCommand]
    private async Task Search()
    {
        var titles = await _catalogService.Search(MangaName);
    }
}