using Avalonia.Controls;

namespace Module.WarhammerTools.Views;

public partial class SimpleLayout : UserControl
{
    public SimpleLayout()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
    }
}