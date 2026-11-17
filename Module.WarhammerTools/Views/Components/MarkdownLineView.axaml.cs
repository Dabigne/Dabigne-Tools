using Avalonia.Controls;
using Avalonia.Interactivity;
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
        LineEditor.AddHandler(InputElement.KeyDownEvent, OnEditorKeyDown, RoutingStrategies.Tunnel);
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

        line.BeginEditing();
        Dispatcher.UIThread.Post(() => FocusEditor());
    }

    private void OnEditorKeyDown(object? sender, KeyEventArgs e)
    {
        if (DataContext is not MarkdownLineViewModel line)
            return;

        var editionView = GetEditionView();
        var editor = LineEditor;
        if (e.Key == Key.Back &&
            e.KeyModifiers == KeyModifiers.None &&
            editor.SelectionStart != editor.SelectionEnd)
            return;

        var navigation = line.HandleKey(e.Key, e.KeyModifiers, editor.CaretIndex);
        if (navigation is null)
            return;

        e.Handled = true;
        FocusLine(editionView, navigation.Value.Line, navigation.Value.CaretIndex);
    }

    private CharacterNoteEditionView? GetEditionView()
    {
        var editionView = this.GetVisualParent();
        while (editionView is not null && editionView is not CharacterNoteEditionView)
            editionView = editionView.GetVisualParent();

        return editionView as CharacterNoteEditionView;
    }

    private void FocusLine(
        CharacterNoteEditionView? editionView,
        MarkdownLineViewModel line,
        int? caretIndex = null)
    {
        Dispatcher.UIThread.Post(() =>
        {
            if (editionView is not null)
            {
                foreach (var lineView in editionView.GetVisualDescendants().OfType<MarkdownLineView>())
                {
                    if (lineView.DataContext == line)
                    {
                        lineView.FocusEditor(caretIndex);
                        break;
                    }
                }
            }
        }, DispatcherPriority.Loaded);
    }
}
