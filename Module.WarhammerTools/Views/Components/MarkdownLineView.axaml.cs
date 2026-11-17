using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Module.WarhammerTools.ViewModels.Components;

namespace Module.WarhammerTools.Views.Components;

public partial class MarkdownLineView : UserControl
{
    public MarkdownLineView()
    {
        InitializeComponent();
    }

    public void FocusEditor(int? caretIndex = null)
    {
        LineEditor.Focus();
        if (caretIndex is not null)
            LineEditor.CaretIndex = Math.Clamp(caretIndex.Value, 0, LineEditor.Text?.Length ?? 0);
    }

    private void OnLinePointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(sender as Control).Properties.PointerUpdateKind != PointerUpdateKind.LeftButtonPressed ||
            DataContext is not MarkdownLineViewModel line ||
            line.IsEditing)
            return;

        var ancestor = this.GetVisualParent();
        while (ancestor is not null && ancestor is not CharacterNoteEditionView)
            ancestor = ancestor.GetVisualParent();

        if (ancestor is not CharacterNoteEditionView { DataContext: CharacterNoteViewModel viewModel })
            return;

        viewModel.BeginEditing(line);
        Dispatcher.UIThread.Post(() => FocusEditor());
    }
}
