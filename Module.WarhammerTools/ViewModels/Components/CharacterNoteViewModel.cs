using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.Services;
using System.Collections.ObjectModel;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterNoteViewModel : ObservableObject
{
    private readonly MarkdownLineEditingService _lineEditingService;
    
    [ObservableProperty]
    private string _title = string.Empty;
    
    public ObservableCollection<MarkdownBlockViewModel> Lines => _lineEditingService.Lines;
    
    public CharacterNoteViewModel(MarkdownLineEditingService lineEditingService)
    {
        _lineEditingService = lineEditingService;
    }
    
    public CharacterNoteViewModel SetModel(Note note)
    {
        Title = note.Title;
        if(note.Blocks.Count == 0)
			_lineEditingService.SetText(note.Text);
		else
			_lineEditingService.SetLines(note.Blocks);
        return this;
    }

    public Note GetModel()
    {
        return new Note
        {
            Title = Title,
            Blocks = Lines.Select(line => new NoteBlock { Text = line.Text }).ToList()
        };
    }
}