using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Path = Avalonia.Controls.Shapes.Path;

namespace Application.Core.UserControls;

[TemplatePart("PART_ChecksPresenter", typeof(ItemsControl))]
public class RatingControl : TemplatedControl
{
    private ItemsControl? _checksPresenter;

    public static readonly StyledProperty<double> CheckSizeProperty = 
        AvaloniaProperty.Register<RatingControl, double>(
            nameof(CheckSize),
            defaultValue:32);

    public double CheckSize
    {
        get => GetValue(CheckSizeProperty);
        set => SetValue(CheckSizeProperty, value);
    }
    
    public static readonly StyledProperty<int> MaxValueProperty = 
        AvaloniaProperty.Register<RatingControl, int>(
            nameof(MaxValue), 
            defaultValue: 3,
            coerce: CoerceMaxValue);

    private static int CoerceMaxValue(AvaloniaObject instance, int value)
    {
        return Math.Max(1, value);
    }

    public int MaxValue
    {
        get => GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }
    
    public static readonly DirectProperty<RatingControl, int> ValueProperty = 
        AvaloniaProperty.RegisterDirect<RatingControl, int>(
            nameof(Value), 
            o => o.Value, 
            (o, v) => o.Value = v,
            defaultBindingMode: BindingMode.TwoWay,
            enableDataValidation: true);

    public int Value
    {
        get => _value;
        set => SetAndRaise(ValueProperty, ref _value, value);
    }
    private int _value;
    
    public static readonly DirectProperty<RatingControl, IEnumerable<int>> ChecksProperty = 
        AvaloniaProperty.RegisterDirect<RatingControl, IEnumerable<int>>(
            nameof(Checks), 
            o => o.Checks, 
            (o, v) => o.Checks = v);

    public IEnumerable<int> Checks
    {
        get => _checks;
        set => SetAndRaise(ChecksProperty, ref _checks, value);
    }
    private IEnumerable<int> _checks = Enumerable.Range(1, 3);

    private void UpdateChecks()
    {
        Checks = Enumerable.Range(1, MaxValue);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        
        if(change.Property == MaxValueProperty)
            UpdateChecks();
    }

    public RatingControl()
    {
        UpdateChecks();
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        if(_checksPresenter is not null)
            _checksPresenter.PointerReleased-= ChecksPresenter_PointerReleased;

        _checksPresenter = e.NameScope.Find("PART_ChecksPresenter") as ItemsControl;
        if(_checksPresenter != null)
            _checksPresenter.PointerReleased += ChecksPresenter_PointerReleased;
    }
    
    private void ChecksPresenter_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
    {
        if (e.Source is not Path check)
            return;
        
        if (Value == 1 && check.DataContext as int? == 1)
        {
            Value = 0;
            return;
        }
            
        Value = check.DataContext as int? ?? 0;
    }
}