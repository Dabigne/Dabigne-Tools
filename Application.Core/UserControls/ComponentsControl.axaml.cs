using System.Collections;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;

namespace Application.Core.UserControls;

public partial class ComponentsControl : UserControl
{
    private IEnumerable _components = new AvaloniaList<object>();
    private bool _isHorizontal = false;

    public static readonly DirectProperty<ComponentsControl, IEnumerable> ComponentsProperty =
        AvaloniaProperty.RegisterDirect<ComponentsControl, IEnumerable>(
            nameof(Components),
            o => o.Components,
            (o, v) => o.Components = v);
    
    public static readonly DirectProperty<ComponentsControl, bool> IsHorizontalProperty =
        AvaloniaProperty.RegisterDirect<ComponentsControl, bool>(
            nameof(IsHorizontal),
            o => o.IsHorizontal,
            (o, v) => o.IsHorizontal = v);
    
    public IEnumerable Components
    {
        get => _components;
        set => SetAndRaise(ComponentsProperty, ref _components, value);
    }

    public bool IsHorizontal
    {
        get => _isHorizontal;
        set => SetAndRaise(IsHorizontalProperty, ref _isHorizontal, value);
    }
    
    public ComponentsControl()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == ComponentsProperty ||  change.Property == IsHorizontalProperty)
            SetComponents();
    }
    
    private void SetComponents()
    {
        InnerGrid.Children.Clear();
        InnerGrid.RowDefinitions.Clear();
        InnerGrid.ColumnDefinitions.Clear();
        
        var index = 0;
        foreach (var component in Components)
        {
            var contentPresenter = new ContentPresenter { Content = component };
            InnerGrid.Children.Add(contentPresenter);

            if (_isHorizontal)
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