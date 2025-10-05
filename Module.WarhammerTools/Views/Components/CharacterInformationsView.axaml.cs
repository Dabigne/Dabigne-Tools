using Avalonia.Controls;

namespace Module.WarhammerTools.Views.Components;

public partial class CharacterInformationsView : UserControl
{
    public CharacterInformationsView()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
    }
}