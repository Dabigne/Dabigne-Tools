using Application.Core.Attributes;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.Types;
using Avalonia.Controls;
using Module.MangaDownload.ViewModels;

namespace Module.MangaDownload.Views;

[Navigatable(Title="Manga Download", Icon="")]
public partial class MangaDownloadView : UserControl, INavigatable
{
    public string Icon { get; } = "Home";
    public string Title { get; } = "Manga Download";
    public string Description { get; } = "Manga Download";

    public MangaDownloadView(IInstanceProvider instanceProvider)
    {
        InitializeComponent();
        DataContext = instanceProvider.GetInstance<MangaDownloadViewModel>();
    }
}