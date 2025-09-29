using System.Collections;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;

namespace Application.Core.UserControls;

public partial class ComponentsControl : UserControl
{
    private IEnumerable _components = new AvaloniaList<object>();
    private bool _isHorizontale = false;

    public static readonly DirectProperty<ComponentsControl, IEnumerable> ComponentsProperty =
        AvaloniaProperty.RegisterDirect<ComponentsControl, IEnumerable>(
            nameof(Components),
            o => o.Components,
            (o, v) => o.Components = v);
    
    public static readonly DirectProperty<ComponentsControl, bool> IsHorizontaleProperty =
        AvaloniaProperty.RegisterDirect<ComponentsControl, bool>(
            nameof(IsHorizontale),
            o => o.IsHorizontale,
            (o, v) => o.IsHorizontale = v);
    
    public IEnumerable Components
    {
        get => _components;
        set => SetAndRaise(ComponentsProperty, ref _components, value);
    }

    public bool IsHorizontale
    {
        get => _isHorizontale;
        set => SetAndRaise(IsHorizontaleProperty, ref _isHorizontale, value);
    }
    
    
    public ComponentsControl()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == ComponentsProperty ||  change.Property == IsHorizontaleProperty)
            SetComponents();
    }
    
    private void SetComponents()
    {
        InnerGrid.RowDefinitions.Clear();
        InnerGrid.ColumnDefinitions.Clear();
        
        var index = 0;
        foreach (var component in Components)
        {
            var contentPresenter = new ContentPresenter { Content = component };
            InnerGrid.Children.Add(contentPresenter);

            if (_isHorizontale)
            {
                InnerGrid.ColumnDefinitions.Add(new ColumnDefinition());
                Grid.SetColumn(contentPresenter, index);
            }
            else
            {
                InnerGrid.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(contentPresenter, index);
            }
            
            index++;
        }
    }
}