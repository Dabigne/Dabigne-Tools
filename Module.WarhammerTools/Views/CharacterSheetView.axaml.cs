using Application.Core.Attributes;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.Types;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Module.WarhammerTools.ViewModels;

namespace Module.WarhammerTools.Views;

[Navigatable(Title="WH-Characters", Icon="")]
public partial class CharacterSheetView : UserControl, INavigatable
{
    public string Icon { get; } = "Home";

    public string Title { get; } = "WH-Characters";
    
    public string Description { get; } = "Display character sheet";

    public CharacterSheetView(IInstanceProvider instanceProvider)
    {
        InitializeComponent();
        DataContext = instanceProvider.GetInstance<CharacterSheetViewModel>();
    }
}