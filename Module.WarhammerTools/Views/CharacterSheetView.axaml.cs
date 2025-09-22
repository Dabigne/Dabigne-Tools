using Application.Core.Attributes;
using Application.Core.Interfaces.Types;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Module.WarhammerTools.Views;

[Navigatable(Title="Characters", Icon="")]
public partial class CharacterSheetView : UserControl, INavigatable
{
    public string Icon { get; } = "Home";

    public string Title { get; } = "Characters";
    
    public string Description { get; } = "Display character sheet";

    public CharacterSheetView()
    {
        InitializeComponent();
    }
}