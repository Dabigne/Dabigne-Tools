using Application.Core.Attributes;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.Types;
using Avalonia.Controls;
using Module.PdfTools.ViewModels;

namespace Module.PdfTools.Views;

[Navigatable(Title="Pdf to MD", Icon="", Folder="PDF")]
public partial class PdfToMarkdownView : UserControl, INavigatable
{
    public string Icon { get; } = "Home";

    public string Title { get; } = "Pdf to MD";
    
    public string Description { get; } = "Pdf to MD";

    public PdfToMarkdownView(IInstanceProvider instanceProvider)
    {
        InitializeComponent();
        DataContext = instanceProvider.GetInstance<PdfToMarkdownViewModel>();
    }
}