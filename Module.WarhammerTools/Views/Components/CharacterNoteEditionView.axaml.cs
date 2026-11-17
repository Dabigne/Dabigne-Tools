using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Module.WarhammerTools.ViewModels.Components;

namespace Module.WarhammerTools.Views.Components;

public partial class CharacterNoteEditionView : UserControl
{
    public CharacterNoteEditionView()
    {
        InitializeComponent();
        MarkdownLines.AddHandler(InputElement.KeyDownEvent, OnEditorKeyDown, RoutingStrategies.Tunnel);
    }

    private void OnEditorKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Source is not TextBox { DataContext: MarkdownLineViewModel line } ||
            DataContext is not CharacterNoteViewModel viewModel)
            return;

        if (e.Key == Key.Enter && !e.KeyModifiers.HasFlag(KeyModifiers.Shift))
        {
            var nextLine = viewModel.SplitLine(line, ((TextBox)e.Source).CaretIndex);
            e.Handled = true;

            if (nextLine is not null)
                FocusLine(nextLine, 0);

            return;
        }

        if (e.KeyModifiers != KeyModifiers.None)
            return;

        var editor = (TextBox)e.Source;
        var caretIndex = editor.CaretIndex;
        var text = line.DraftText;

        if (e.Key == Key.Back && e.KeyModifiers == KeyModifiers.None && caretIndex == 0)
        {
            var previousLine = viewModel.MergeWithPreviousLine(line, out var previousCaretIndex);
            if (previousLine is not null)
            {
                e.Handled = true;
                FocusLine(previousLine, previousCaretIndex);
            }

            return;
        }

        var lineStart = caretIndex == 0 ? -1 : text.LastIndexOf('\n', caretIndex - 1);
        var lineEnd = text.IndexOf('\n', caretIndex);

        int lineOffset;
        int targetCaretIndex;
        switch (e.Key)
        {
            case Key.Up when lineStart < 0:
                lineOffset = -1;
                targetCaretIndex = caretIndex;
                break;
            case Key.Down when lineEnd < 0:
                lineOffset = 1;
                targetCaretIndex = caretIndex - (lineStart + 1);
                break;
            case Key.Left when caretIndex == 0:
                lineOffset = -1;
                targetCaretIndex = int.MaxValue;
                break;
            case Key.Right when caretIndex == text.Length:
                lineOffset = 1;
                targetCaretIndex = 0;
                break;
            default:
                return;
        }

        var targetLine = viewModel.NavigateToLine(line, lineOffset, targetCaretIndex, out var resolvedCaretIndex);
        if (targetLine is null)
            return;

        e.Handled = true;
        FocusLine(targetLine, resolvedCaretIndex);
    }

    private void FocusLine(MarkdownLineViewModel line, int? caretIndex = null)
    {
        Dispatcher.UIThread.Post(() =>
        {
            foreach (var lineView in this.GetVisualDescendants().OfType<MarkdownLineView>())
            {
                if (lineView.DataContext == line)
                {
                    lineView.FocusEditor(caretIndex);
                    break;
                }
            }
        });
    }
}