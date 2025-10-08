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