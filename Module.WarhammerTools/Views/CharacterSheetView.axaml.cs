using Application.Core.Attributes;
using Application.Core.Interfaces.Types;
using Avalonia.Controls;
using Module.WarhammerTools.Services;
using Module.WarhammerTools.ViewModels;

namespace Module.WarhammerTools.Views;

[Navigatable(Title="WH-Characters", Icon="")]
public partial class CharacterSheetView : UserControl, INavigatable, IParameterizable
{
    public string Icon => "Home";

    public string Title => "WH-Characters";

    public string Description => "Display character sheet";

    public string? Parameter
    {
        get => (DataContext as CharacterSheetViewModel)?.LastFilePath;
        set => (DataContext as CharacterSheetViewModel)?.LoadCharacterSheet(value);
    }

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