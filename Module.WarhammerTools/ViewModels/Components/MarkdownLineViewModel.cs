using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Services;

namespace Module.WarhammerTools.ViewModels.Components;

public readonly record struct MarkdownLineNavigation(MarkdownLineViewModel Line, int CaretIndex);

public partial class MarkdownLineViewModel(
    string text,
    MarkdownLineEditingService editingService) : ObservableObject
{
    [ObservableProperty]
    private string _text = text;

    [ObservableProperty]
    private string _draftText = text;

    [ObservableProperty]
    private bool _isEditing;

    public bool IsPreviewing => !IsEditing;

    partial void OnIsEditingChanged(bool value)
    {
        OnPropertyChanged(nameof(IsPreviewing));
    }

    public void BeginEditing() => editingService.BeginEditing(this);

    public MarkdownLineNavigation? HandleKey(Key key, KeyModifiers modifiers, int caretIndex)
    {
        if (key == Key.Enter && !modifiers.HasFlag(KeyModifiers.Shift))
        {
            var nextLine = editingService.SplitLine(this, caretIndex);
            return nextLine is null ? null : new MarkdownLineNavigation(nextLine, 0);
        }

        if (modifiers != KeyModifiers.None)
            return null;

        if (key == Key.Back && caretIndex == 0)
        {
            var previousLine = editingService.MergeWithPreviousLine(this, out var previousCaretIndex);
            return previousLine is null
                ? null
                : new MarkdownLineNavigation(previousLine, previousCaretIndex);
        }

        var text = DraftText;
        var lineStart = caretIndex == 0 ? -1 : text.LastIndexOf('\n', caretIndex - 1);
        var lineEnd = text.IndexOf('\n', caretIndex);

        int lineOffset;
        int targetCaretIndex;
        switch (key)
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
                return null;
        }

        var targetLine = editingService.NavigateToLine(
            this,
            lineOffset,
            targetCaretIndex,
            out var resolvedCaretIndex);
        return targetLine is null
            ? null
            : new MarkdownLineNavigation(targetLine, resolvedCaretIndex);
    }
}
