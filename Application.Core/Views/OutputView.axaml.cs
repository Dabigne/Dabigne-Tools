using System.Collections.Specialized;
using Avalonia.Controls;
using Avalonia.Threading;

namespace Application.Core.Views;

public partial class OutputView : UserControl
{
    public OutputView()
    {
        InitializeComponent();
        
        ListBox.Items.CollectionChanged += ItemsOnCollectionChanged;
    }

    private void ItemsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            var scrollViewer = ListBox.Scroll as ScrollViewer;
            scrollViewer?.ScrollToEnd();
        }
    }
}