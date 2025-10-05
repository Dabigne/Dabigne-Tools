using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Components;

namespace Module.WarhammerTools.ViewModels;

public sealed class CharacterSheetViewModel : ObservableObject
{
    private readonly CharacterInformationsViewModel _informations = new();
    private readonly CharacterCharacteristicListViewModel _characteristicList = new();
    private readonly CharacterDestinyViewModel _destiny = new();
    private readonly CharacterResilienceViewModel _resilience = new();
    private readonly CharacterExperienceViewModel _experience = new();
    private readonly CharacterMovementViewModel _movement = new();
    
    public List<ObservableObject> BodyComponents { get; private set; }
    
    public CharacterSheetViewModel(ICharacterSheetService characterSheetService)
    {
        BodyComponents = 
        [
            _informations,
            _characteristicList,
            _destiny,
            _resilience,
            _experience,
            _movement
        ];
        
        SetModel(characterSheetService.BuildCharacterSheet());
    }

    private void SetModel(CharacterSheet characterSheet)
    {
        _informations.SetModel(characterSheet.Informations);
        _characteristicList.SetModel(characterSheet.Characteristics);
        _destiny.SetModel(characterSheet.Destiny);
        _resilience.SetModel(characterSheet.Resilience);
        _experience.SetModel(characterSheet.Experience);
        _movement.SetModel(characterSheet.Movement);
    }
}