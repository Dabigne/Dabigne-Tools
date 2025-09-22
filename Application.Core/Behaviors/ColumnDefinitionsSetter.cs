using Avalonia;
using Avalonia.Controls;

namespace Application.Core.Behaviors;

public class ColumnDefinitionsSetter : AvaloniaObject
{
    public static readonly AttachedProperty<ColumnDefinitions> ColumnDefinitionsProperty =
        AvaloniaProperty.RegisterAttached<ColumnDefinitionsSetter, ColumnDefinitions>("ColumnDefinitions", typeof(ColumnDefinitionsSetter));

    public static void SetColumnDefinitions(AvaloniaObject element, ColumnDefinitions value) =>
        element.SetValue(ColumnDefinitionsProperty, value);
    public static ColumnDefinitions GetColumnDefinitions(AvaloniaObject element) =>
        element.GetValue(ColumnDefinitionsProperty);

    static ColumnDefinitionsSetter()
    {
        ColumnDefinitionsProperty.Changed.AddClassHandler<Grid, ColumnDefinitions>((grid, e) =>
        {
            grid.ColumnDefinitions.Clear();

            if (e.NewValue.GetValueOrDefault() is { } columns)
                grid.ColumnDefinitions.AddRange(columns.Select(o => new ColumnDefinition()
                {
                    Width = o.Width,
                    SharedSizeGroup = o.SharedSizeGroup,
                }));
        });
    }
}
