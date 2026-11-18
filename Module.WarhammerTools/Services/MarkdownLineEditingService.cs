using System.Collections.ObjectModel;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Components;

namespace Module.WarhammerTools.Services;

public sealed class MarkdownLineEditingService
{
    private MarkdownBlockViewModel? _editingLine;

    public ObservableCollection<MarkdownBlockViewModel> Lines { get; } = [];
    
    public MarkdownLineEditingService()
    {
        Lines.Add(new MarkdownBlockViewModel(string.Empty, this));
    }

    public void SetText(string text)
    {
        _editingLine = null;
        Lines.Clear();

        foreach (var line in text.Replace("\r\n", "\n").Split('\n'))
            Lines.Add(new MarkdownBlockViewModel(line, this));
    }
    
    public void SetLines(IList<NoteBlock> lines)
	{
		_editingLine = null;
		Lines.Clear();

		foreach (var line in lines)
			Lines.Add(new MarkdownBlockViewModel(line.Text, this));
	}

    public void BeginEditing(MarkdownBlockViewModel block)
    {
        if (!Lines.Contains(block))
            return;

        if (_editingLine is not null && _editingLine != block)
            CommitLine(_editingLine);

        _editingLine = block;
        block.DraftText = block.Text;
        block.IsEditing = true;
    }

    public MarkdownBlockViewModel? NavigateToLine(
        MarkdownBlockViewModel currentBlock,
        int lineOffset,
        int caretColumn,
        out int targetCaretIndex)
    {
        targetCaretIndex = 0;
        var currentIndex = Lines.IndexOf(currentBlock);
        var targetIndex = currentIndex + lineOffset;
        if (currentIndex < 0 || targetIndex < 0 || targetIndex >= Lines.Count)
            return null;

        var targetLine = Lines[targetIndex];
        targetCaretIndex = Math.Clamp(caretColumn, 0, targetLine.Text.Length);
        BeginEditing(targetLine);
        return targetLine;
    }

    public MarkdownBlockViewModel? MergeWithPreviousLine(
        MarkdownBlockViewModel block,
        out int targetCaretIndex)
    {
        targetCaretIndex = 0;
        var lineIndex = Lines.IndexOf(block);
        if (lineIndex <= 0 || block != _editingLine)
            return null;

        var previousLine = Lines[lineIndex - 1];
        targetCaretIndex = previousLine.Text.Length;
        previousLine.Text += block.DraftText;
        previousLine.DraftText = previousLine.Text;
        block.IsEditing = false;
        Lines.RemoveAt(lineIndex);
        _editingLine = null;
        BeginEditing(previousLine);
        return previousLine;
    }

    public MarkdownBlockViewModel? SplitLine(MarkdownBlockViewModel block, int caretIndex)
    {
        var lineIndex = Lines.IndexOf(block);
        if (lineIndex < 0 || block != _editingLine)
            return null;

        caretIndex = Math.Clamp(caretIndex, 0, block.DraftText.Length);
        var textAfterCaret = block.DraftText[caretIndex..];
        block.Text = block.DraftText[..caretIndex];
        block.IsEditing = false;
        _editingLine = null;

        var nextLine = new MarkdownBlockViewModel(textAfterCaret, this);
        Lines.Insert(lineIndex + 1, nextLine);
        BeginEditing(nextLine);
        return nextLine;
    }

    public MarkdownBlockViewModel? CommitLine(MarkdownBlockViewModel block, bool editNextLine = false)
    {
        if (block != _editingLine)
            return null;

        var lineIndex = Lines.IndexOf(block);
        block.Text = block.DraftText;
        block.IsEditing = false;
        _editingLine = null;

        if (!editNextLine)
            return null;

        if (lineIndex == Lines.Count - 1)
        {
            Lines.Add(new MarkdownBlockViewModel(string.Empty, this));
        }

        var nextLine = Lines[lineIndex + 1];
        BeginEditing(nextLine);
        return nextLine;
    }

    public void CancelLine(MarkdownBlockViewModel block)
    {
        if (block != _editingLine)
            return;

        block.DraftText = block.Text;
        block.IsEditing = false;
        _editingLine = null;
    }

}
