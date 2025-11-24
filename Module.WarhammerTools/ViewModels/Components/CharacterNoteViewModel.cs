using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public enum NoteViewType
{
    Both,
    View,
    Edit
}

public partial class CharacterNoteViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;
    
    [ObservableProperty]
    private string _text = string.Empty;
    
    [ObservableProperty]
    private NoteViewType _viewType = NoteViewType.Both;
    
    [ObservableProperty] 
    private GridLength _editionWidth = GridLength.Star;

    [ObservableProperty] 
    private GridLength _viewWidth = GridLength.Star;
    
    public IList<NoteViewType> ViewTypes { get; } 
        = [NoteViewType.Both, NoteViewType.View, NoteViewType.Edit];

    public CharacterNoteViewModel()
    {
        ViewType = ViewTypes[0];
    }

    partial void OnViewTypeChanged(NoteViewType value)
    {
        switch (value)
        {
            case NoteViewType.Both:
                ViewWidth = new GridLength(0.5, GridUnitType.Star);
                EditionWidth = new GridLength(0.5, GridUnitType.Star);
                break;
            case NoteViewType.View:
                ViewWidth = new GridLength(1 , GridUnitType.Star);
                EditionWidth = new GridLength(0, GridUnitType.Star);
                break;
            case NoteViewType.Edit:
                ViewWidth = new GridLength(0, GridUnitType.Star);
                EditionWidth = new GridLength(1, GridUnitType.Star);
                break;
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