using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterNoteViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title;
    
    [ObservableProperty]
    private string _text;
    
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