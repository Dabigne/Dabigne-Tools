using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;
using System.Collections.ObjectModel;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterNoteViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;
    
    [ObservableProperty]
    private string _text = string.Empty;

    public ObservableCollection<MarkdownLineViewModel> Lines { get; } =
        [new MarkdownLineViewModel(string.Empty)];

    public MarkdownLineViewModel? EditingLine => _editingLine;

    private MarkdownLineViewModel? _editingLine;
    private bool _updatingTextFromLines;

    partial void OnTextChanged(string value)
    {
        if (!_updatingTextFromLines)
            RefreshLines();
    }

    public void BeginEditing(MarkdownLineViewModel line)
    {
        if (!Lines.Contains(line))
            return;

        if (_editingLine is not null && _editingLine != line)
            CommitLine(_editingLine);

        _editingLine = line;
        _editingLine.DraftText = _editingLine.Text;
        _editingLine.IsEditing = true;
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

        var nextLine = new MarkdownLineViewModel(textAfterCaret);
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
            Lines.Add(new MarkdownLineViewModel(string.Empty));
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

    private void RefreshLines()
    {
        _editingLine = null;
        Lines.Clear();

        foreach (var line in Text.Replace("\r\n", "\n").Split('\n'))
            Lines.Add(new MarkdownLineViewModel(line));
    }

    private void UpdateTextFromLines()
    {
        _updatingTextFromLines = true;
        try
        {
            Text = string.Join("\n", Lines.Select(item => item.Text));
        }
        finally
        {
            _updatingTextFromLines = false;
        }
    }

    public CharacterNoteViewModel SetModel(Note note)
    {
        Title = note.Title;
        Text = note.Text;
        return this;
    }

    public Note GetModel()
    {
        return new Note
        {
            Title = Title,
            Text = Text
        };
    }
}