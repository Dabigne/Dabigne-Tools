using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.Services;
using System.Collections.ObjectModel;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterNoteViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;
    
    [ObservableProperty]
    private string _text = string.Empty;

    private readonly MarkdownLineEditingService _lineEditingService;

    public ObservableCollection<MarkdownLineViewModel> Lines => _lineEditingService.Lines;

    private bool _updatingTextFromLineEditing;

    public CharacterNoteViewModel(MarkdownLineEditingService lineEditingService)
    {
        _lineEditingService = lineEditingService;
        _lineEditingService.TextUpdated += OnLineEditingTextUpdated;
    }

    partial void OnTextChanged(string value)
    {
        if (!_updatingTextFromLineEditing)
            _lineEditingService.SetText(value);
    }

    private void OnLineEditingTextUpdated(string text)
    {
        _updatingTextFromLineEditing = true;
        try
        {
            Text = text;
        }
        finally
        {
            _updatingTextFromLineEditing = false;
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