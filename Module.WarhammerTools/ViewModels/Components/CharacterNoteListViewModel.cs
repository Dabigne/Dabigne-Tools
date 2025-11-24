using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterNoteListViewModel : ObservableObject
{
    public ObservableCollection<CharacterNoteViewModel> Items { get; } = [];
    
    [ObservableProperty]
    private CharacterNoteViewModel? _selectedNote = new();

    [RelayCommand]
    private void Add()
    {
        var note = new CharacterNoteViewModel();
        Items.Add(note);
        SelectedNote = note;
    }

    [RelayCommand]
    private void Remove()
    {
        if (SelectedNote is null)
            return;
        Items.Remove(SelectedNote);
    }

    public void SetModel(IList<Note> notes)
    {
        Items.Clear();
        foreach (var note in notes)
        {
            Items.Add(new CharacterNoteViewModel().SetModel(note));
        }
    }

    public IList<Note> GetModel()
    {
        return Items.Select(n => n.GetModel()).ToList();
    }
}