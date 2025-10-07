using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterExpertiseViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _name;
    
    [ObservableProperty]
    private string _characteristic;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Value))]
    private int _characteristicValue;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Value))]
    private int _improvement;

    public int Value => CharacteristicValue + Improvement;

    public void SetModel(CharacterExpertise expertise, CharacterCharacteristic characteristic)
    {
        Name = expertise.Name;
        Characteristic = expertise.Characteristic;
        CharacteristicValue = characteristic.Value;
        Improvement = expertise.Improvement;
    }

    public CharacterExpertise GetModel()
    {
        return new CharacterExpertise
        {
            Name = Name,
            Characteristic = Characteristic,
            Improvement = Improvement
        };
    }
}

public partial class CharacterExpertiseListViewModel : ObservableObject
{
    [ObservableProperty]
    private IList<CharacterExpertiseViewModel> _list = [];

    public void SetModel(
        IList<CharacterExpertise> expertises, 
        IList<CharacterCharacteristic> characteristics)
    {
        var newList = new List<CharacterExpertiseViewModel>();
        foreach (var modelItem in expertises)
        {
            var vm = new CharacterExpertiseViewModel();
            vm.SetModel(modelItem, characteristics.FirstOrDefault(c => c.ShortCut == modelItem.Characteristic));
            newList.Add(vm);
        }
        
        List =  newList;
    }

    public IList<CharacterExpertise> GetModel()
    {
        return List.Select(i => i.GetModel()).ToList();
    }
}