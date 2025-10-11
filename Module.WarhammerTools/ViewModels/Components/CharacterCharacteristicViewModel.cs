using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterCharacteristicViewModel : ObservableObject, IViewModel<CharacterCharacteristic>
{
    [ObservableProperty]
    private string _name = string.Empty;
    
    [ObservableProperty]
    private string _shortcut = string.Empty;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentValue))]
    private int _initialValue;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentValue))]
    private int _improvements;

    public int CurrentValue => InitialValue + Improvements;
    
    public void SetModel(CharacterCharacteristic model)
    {
        Name = model.Name;
        Shortcut = model.ShortCut;
        InitialValue = model.InitialValue;
        Improvements = model.Improvments;
    }

    public CharacterCharacteristic GetModel()
    {
        return new CharacterCharacteristic(Name, Shortcut, InitialValue, Improvements);
    }
}