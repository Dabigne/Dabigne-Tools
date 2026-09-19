using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterExpertiseViewModel : ObservableObject, IViewModel<CharacterExpertise>
{
    private readonly ICharacterSheetService _characterSheetService;
    
    [ObservableProperty] 
    private string _name = string.Empty;
    
    [ObservableProperty]
    private string _characteristic = string.Empty;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Value))]
    private int _characteristicValue;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Value))]
    private int _improvement;

    public int Value => CharacteristicValue + Improvement;

    public CharacterExpertiseViewModel(ICharacterSheetService characterSheetService)
    {
        _characterSheetService = characterSheetService;
        _characterSheetService.CharacterSheetChanged += CharacterSheetServiceOnCharacterSheetChanged;
    }

    private void CharacterSheetServiceOnCharacterSheetChanged()
    {
	    CharacteristicValue = GetCharacteristic(Characteristic).Value;
    }

    public void SetModel(CharacterExpertise expertise)
    {
        Name = expertise.Name;
        Characteristic = expertise.Characteristic;
        CharacteristicValue = GetCharacteristic(Characteristic).Value;
        Improvement = expertise.Improvement;
    }

    private CharacterCharacteristic GetCharacteristic(string characteristicShortcut)
    {
	    return  _characterSheetService
		    .GetLoadedCharacterSheet()!
		    .Characteristics
		    .First(c => c.ShortCut == characteristicShortcut);
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