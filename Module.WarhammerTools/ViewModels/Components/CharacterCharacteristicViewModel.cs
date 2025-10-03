using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

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
    private int _improvements;

    public int CurrentValue => InitialValue + Improvements;
    
    public void SetModel(CharacterCharacteristic model)
    {
        Name = model.Name;
        Shortcut = model.ShortCut;
        InitialValue = model.InitialValue;
        Improvements = model.Improvments;
    }
}

public partial class CharacterCharacteristicListViewModel : ObservableObject
{
    public List<CharacterCharacteristicViewModel> List { get; } = [];

    public void SetModel(IList<CharacterCharacteristic> characteristics)
    {
        List.Clear();
        foreach (var characteristic in characteristics)
        {
            var vm = new CharacterCharacteristicViewModel();
            vm.SetModel(characteristic);
            List.Add(vm);
        }
    }
}