using System.Collections.ObjectModel;
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

    public CharacterCharacteristic GetModel()
    {
        return new CharacterCharacteristic(Name, Shortcut, InitialValue, Improvements);
    }
}

public partial class CharacterCharacteristicListViewModel : ObservableObject
{
    [ObservableProperty]
    private IList<CharacterCharacteristicViewModel> _list  = [];

    public void SetModel(IList<CharacterCharacteristic> characteristics)
    {
        var newList = new List<CharacterCharacteristicViewModel>();
        foreach (var characteristic in characteristics)
        {
            var vm = new CharacterCharacteristicViewModel();
            vm.SetModel(characteristic);
            newList.Add(vm);
        }
        
        List = newList;
    }

    public IList<CharacterCharacteristic> GetModel()
    {
        return List.Select(viewModel => viewModel.GetModel()).ToList();
    }
}