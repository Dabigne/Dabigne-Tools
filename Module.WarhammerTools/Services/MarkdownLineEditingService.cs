using System.Collections.ObjectModel;
using Module.WarhammerTools.ViewModels.Components;

namespace Module.WarhammerTools.Services;

public sealed class MarkdownLineEditingService
{
    private MarkdownLineViewModel? _editingLine;

    public ObservableCollection<MarkdownLineViewModel> Lines { get; } = [];

    public event Action<string>? TextUpdated;

    public MarkdownLineEditingService()
    {
        Lines.Add(new MarkdownLineViewModel(string.Empty, this));
    }

    public void SetText(string text)
    {
        _editingLine = null;
        Lines.Clear();

        foreach (var line in text.Replace("\r\n", "\n").Split('\n'))
            Lines.Add(new MarkdownLineViewModel(line, this));
    }

    public void BeginEditing(MarkdownLineViewModel line)
    {
        if (!Lines.Contains(line))
            return;

        if (_editingLine is not null && _editingLine != line)
            CommitLine(_editingLine);

        _editingLine = line;
        line.DraftText = line.Text;
        line.IsEditing = true;
    }

    public MarkdownLineViewModel? NavigateToLine(
        MarkdownLineViewModel currentLine,
        int lineOffset,
        int caretColumn,
        out int targetCaretIndex)
    {
        targetCaretIndex = 0;
        var currentIndex = Lines.IndexOf(currentLine);
        var targetIndex = currentIndex + lineOffset;
        if (currentIndex < 0 || targetIndex < 0 || targetIndex >= Lines.Count)
            return null;

        var targetLine = Lines[targetIndex];
        targetCaretIndex = Math.Clamp(caretColumn, 0, targetLine.Text.Length);
        BeginEditing(targetLine);
        return targetLine;
    }

    public MarkdownLineViewModel? MergeWithPreviousLine(
        MarkdownLineViewModel line,
        out int targetCaretIndex)
    {
        targetCaretIndex = 0;
        var lineIndex = Lines.IndexOf(line);
        if (lineIndex <= 0 || line != _editingLine)
            return null;

        var previousLine = Lines[lineIndex - 1];
        targetCaretIndex = previousLine.Text.Length;
        previousLine.Text += line.DraftText;
        previousLine.DraftText = previousLine.Text;
        line.IsEditing = false;
        Lines.RemoveAt(lineIndex);
        _editingLine = null;
        UpdateTextFromLines();
        BeginEditing(previousLine);
        return previousLine;
    }

    public MarkdownLineViewModel? SplitLine(MarkdownLineViewModel line, int caretIndex)
    {
        var lineIndex = Lines.IndexOf(line);
        if (lineIndex < 0 || line != _editingLine)
            return null;

        caretIndex = Math.Clamp(caretIndex, 0, line.DraftText.Length);
        var textAfterCaret = line.DraftText[caretIndex..];
        line.Text = line.DraftText[..caretIndex];
        line.IsEditing = false;
        _editingLine = null;

        var nextLine = new MarkdownLineViewModel(textAfterCaret, this);
        Lines.Insert(lineIndex + 1, nextLine);
        UpdateTextFromLines();
        BeginEditing(nextLine);
        return nextLine;
    }

    public MarkdownLineViewModel? CommitLine(MarkdownLineViewModel line, bool editNextLine = false)
    {
        if (line != _editingLine)
            return null;

        var lineIndex = Lines.IndexOf(line);
        line.Text = line.DraftText;
        line.IsEditing = false;
        _editingLine = null;
        UpdateTextFromLines();

        if (!editNextLine)
            return null;

        if (lineIndex == Lines.Count - 1)
        {
            Lines.Add(new MarkdownLineViewModel(string.Empty, this));
            UpdateTextFromLines();
        }

        var nextLine = Lines[lineIndex + 1];
        BeginEditing(nextLine);
        return nextLine;
    }

    public void CancelLine(MarkdownLineViewModel line)
    {
        if (line != _editingLine)
            return;

        line.DraftText = line.Text;
        line.IsEditing = false;
        _editingLine = null;
    }

    private void UpdateTextFromLines()
    {
        TextUpdated?.Invoke(string.Join("\n", Lines.Select(item => item.Text)));
    }
}
