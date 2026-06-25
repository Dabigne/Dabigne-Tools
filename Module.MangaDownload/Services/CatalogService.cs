using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Application.Core.Interfaces.Services;
using Application.Core.Models;
using Avalonia.Media;
using Module.MangaDownload.Interfaces;

namespace Module.MangaDownload.Services;

public class CatalogService : ICatalogService
{
    private readonly string _uri = $"{UrlConstants.SiteBaseUrl}catalogue/?type%5B%5D=Scans&search=";
    private const string TitleSelector = "h2.card-title";
    private readonly IOutputService _outputService;

    public CatalogService(IOutputService outputService)
    {
        _outputService = outputService;
    }
    
    public async Task<IEnumerable<string>> Search(string query)
    {
        _outputService.Push(new OutputLine($"Searching for {query}"));
        using var httpClient = new HttpClient();
        
        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);
        var document = await context.OpenAsync($"{_uri}{query}");
        var result = document.QuerySelectorAll<IHtmlHeadingElement>(TitleSelector);
        var titles = result.Select(r => r.InnerHtml).ToList();
        
        _outputService.Push(new OutputLine($"Found {titles.Count} title(s)"));
        foreach (var title in titles)
        {
            _outputService.Push(new OutputLine(title, true, Colors.LimeGreen));
        }
        
        return titles;
    }
}