using Application.Core.Attributes;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.Types;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Module.PdfTools.ViewModels;

namespace Module.PdfTools.Views;

[Navigatable(Title="Pdf merge", Icon="")]
public partial class PdfMergeView : UserControl, INavigatable
{
    public string Icon { get; } = "Home";

    public string Title { get; } = "Pdf merge";
    
    public string Description { get; } = "Merge Pdf into one";

    public PdfMergeView(IInstanceProvider instanceProvider)
    {
        InitializeComponent();
        DataContext = instanceProvider.GetInstance<PdfMergeViewModel>();
    }
}