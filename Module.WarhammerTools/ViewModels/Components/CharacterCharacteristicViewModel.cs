using CommunityToolkit.Mvvm.ComponentModel;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterCharacteristicViewModel : ObservableObject
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
    private int _improvments;

    public int CurrentValue => InitialValue + Improvments;

    public CharacterCharacteristicViewModel(string name, string shortcut)
    {
        Name = name;
        Shortcut = shortcut;
    }
}

public partial class CharacterCharacteristicListViewModel : ObservableObject
{
    public List<CharacterCharacteristicViewModel> List { get; } =
    [
        new CharacterCharacteristicViewModel("Compétence Combat", "CC"),
        new CharacterCharacteristicViewModel("Compétence Tir", "CT"),
        new CharacterCharacteristicViewModel("Force", "F"),
    ];
}