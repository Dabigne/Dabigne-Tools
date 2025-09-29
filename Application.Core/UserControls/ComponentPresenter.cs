// using Avalonia;
// using Avalonia.Controls;
// using Avalonia.Controls.Presenters;
//
// namespace Application.Core.UserControls;
//
// public class ComponentPresenter : UserControl
// {
//     public static readonly StyledProperty<IList<object>?> ComponentsSourceProperty = 
//         AvaloniaProperty.Register<ComponentPresenter, IList<object>?>(nameof(ComponentsSource));
//
//     public IList<object>? ComponentsSource
//     {
//         get => GetValue(ComponentsSourceProperty);
//         set => SetValue(ComponentsSourceProperty, value);
//     }
//     
//     private readonly Grid _innerGrid = new Grid();
//
//     public ComponentPresenter()
//     {
//         VisualChildren.Add(_innerGrid);
//         ComponentsSourceProperty.Changed.AddClassHandler<ComponentPresenter>(OnComponentsChanged);
//     }
//
//     private void OnComponentsChanged(ComponentPresenter sender, AvaloniaPropertyChangedEventArgs args)
//     {
//         sender.SetComponents();
//     }
//     
//     public void SetComponents()
//     {
//         _innerGrid.RowDefinitions.Clear();
//         
//         if(ComponentsSource == null)
//             return;
//         
//         foreach (var component in ComponentsSource)
//         {
//             _innerGrid.RowDefinitions.Add(new RowDefinition());
//             var contentPresenter = new ContentPresenter
//             {
//                 Content = component
//             };
//             _innerGrid.Children.Add(contentPresenter);
//         }
//     }
// }