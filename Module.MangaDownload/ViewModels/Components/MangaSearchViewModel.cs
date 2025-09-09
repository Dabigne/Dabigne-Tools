using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.MangaDownload.Interfaces;

namespace Module.MangaDownload.ViewModels.Components;

public partial class MangaSearchViewModel : ObservableObject
{
    private readonly ICatalogService _catalogService;
    private readonly IOutputService  _outputService;

    [ObservableProperty] 
    private string _mangaName = string.Empty;

    public MangaSearchViewModel(ICatalogService catalogService, IOutputService outputService)
    {
        _catalogService = catalogService;
        _outputService = outputService;
        _outputService.LineSelected += OutputServiceOnLineSelected;
    }

    private void OutputServiceOnLineSelected(IOutputLine line)
    {
        MangaName = line.Content;
    }

    [RelayCommand]
    private async Task Search()
    {
        var titles = await _catalogService.Search(MangaName);
    }
}