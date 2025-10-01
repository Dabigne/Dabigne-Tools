using Application.Core.Attributes;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.Types;
using Avalonia.Controls;

namespace Module.PdfTools.Views;

[Navigatable(Title="Pdf from Images", Icon="", Folder="PDF")]
public partial class PdfFromImagesView : UserControl, INavigatable
{
    public string Icon { get; } = "Home";

    public string Title { get; } = "Pdf from Images";
    
    public string Description { get; } = "Pdf from Images";

    public PdfFromImagesView(IInstanceProvider  instanceProvider)
    {
        InitializeComponent();
    }
}