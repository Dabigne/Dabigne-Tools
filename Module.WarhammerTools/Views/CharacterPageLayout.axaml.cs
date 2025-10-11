using Avalonia.Controls;

namespace Module.WarhammerTools.Views;

public partial class CharacterPageLayout : UserControl
{
    public CharacterPageLayout()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
    }
}