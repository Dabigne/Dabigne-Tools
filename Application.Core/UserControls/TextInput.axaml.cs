using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Application.Core.UserControls;

public partial class TextInput : UserControl
{
    public static readonly DirectProperty<TextInput, string> HeaderProperty =
        AvaloniaProperty.RegisterDirect<TextInput, string>(
            nameof(Header),
            o => o.Header,
            (o, v) => o.Header = v);

    public static readonly DirectProperty<TextInput, string> TextProperty =
        AvaloniaProperty.RegisterDirect<TextInput, string>(
            nameof(Text),
            o => o.Text,
            (o, v) => o.Text = v);

    public string Header
    {
        get => _header;
        set => SetAndRaise(HeaderProperty, ref _header, value);
    }
    private string _header;
    
    public string Text
    {
        get => _text;
        set => SetAndRaise(TextProperty, ref _text, value);
    }
    private string _text;

    public TextInput()
    {
        InitializeComponent();
    }
}