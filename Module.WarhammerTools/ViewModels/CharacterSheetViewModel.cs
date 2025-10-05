using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Components;

namespace Module.WarhammerTools.ViewModels;

public sealed partial class CharacterSheetViewModel : ObservableObject
{
    [ObservableProperty]
    private CharacterInformationsViewModel _informations;
    
    public CharacterCharacteristicListViewModel CharacteristicList { get; }  = new();
    
    public CharacterDestinyViewModel Destiny { get; }  = new();
    
    public CharacterResilienceViewModel Resilience { get; }  = new();
    
    public CharacterExperienceViewModel Experience { get; }  = new();
    
    public CharacterMovementViewModel Movement { get; }  = new();
    
    public CharacterSheetViewModel(ICharacterSheetService characterSheetService)
    {
        Informations = new CharacterInformationsViewModel();
        
        SetModel(characterSheetService.BuildCharacterSheet());
    }

    private void SetModel(CharacterSheet characterSheet)
    {
        Informations.SetModel(characterSheet.Informations);
        CharacteristicList.SetModel(characterSheet.Characteristics);
        Destiny.SetModel(characterSheet.Destiny);
        Resilience.SetModel(characterSheet.Resilience);
        Experience.SetModel(characterSheet.Experience);
        Movement.SetModel(characterSheet.Movement);
    }
}