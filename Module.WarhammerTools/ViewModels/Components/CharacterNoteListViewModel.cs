using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.WarhammerTools.Models;
using Application.Core.Interfaces.Services;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterNoteListViewModel : ObservableObject
{
    private readonly IInstanceProvider _instanceProvider;

    public ObservableCollection<CharacterNoteViewModel> Items { get; } = [];
    
    [ObservableProperty]
    private CharacterNoteViewModel? _selectedNote;

    public CharacterNoteListViewModel(IInstanceProvider instanceProvider)
    {
        _instanceProvider = instanceProvider;
        SelectedNote = _instanceProvider.GetInstance<CharacterNoteViewModel>();
    }

    [RelayCommand]
    private void Add()
    {
        var note = _instanceProvider.GetInstance<CharacterNoteViewModel>();
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
            Items.Add(_instanceProvider.GetInstance<CharacterNoteViewModel>().SetModel(note));
        }
    }

    public IList<Note> GetModel()
    {
        return Items.Select(n => n.GetModel()).ToList();
    }
}