using Application.Core.Attributes;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.Types;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Templates;
using Module.WarhammerTools.Services;
using Module.WarhammerTools.ViewModels;
using Module.WarhammerTools.ViewModels.Components;
using Module.WarhammerTools.Views.Components;

namespace Module.WarhammerTools.Views;

[Navigatable(Title="WH-Characters", Icon="")]
public partial class CharacterSheetView : UserControl, INavigatable
{
    public string Icon { get; } = "Home";

    public string Title { get; } = "WH-Characters";
    
    public string Description { get; } = "Display character sheet";

    public CharacterSheetView(
        TemplateService templateService,
        CharacterSheetViewModel viewModel)
    {
        InitializeComponent();

        var templates = templateService.GetTemplates();
        DataTemplates.AddRange(templates);
        
        DataContext = viewModel;
    }
}