using Application.Core.Attributes;
using Application.Core.Interfaces.Types;
using Avalonia.Controls;
using Module.WorkShopTools.ViewModels;

namespace Module.WorkShopTools.Views;

[Navigatable(Title="Mode Explorer", Icon="")]
public partial class ModeExplorerView : UserControl, INavigatable
{
    public string Icon => "Home";

    public string Title => "Mode Explorer";

    public string Description => "Explore a TTS mode";

    public ModeExplorerView(ModeExplorerViewModel  viewModeExplorerViewModel)
    {
        InitializeComponent();
        
        DataContext = viewModeExplorerViewModel;
    }
}