using Avalonia.Media;
using Avalonia.Media.Fonts;
using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Components;

namespace Module.WarhammerTools.ViewModels;

public sealed class CharacterSheetViewModel : ObservableObject
{
    private readonly CharacterInformationsViewModel _informations = new();
    private readonly CharacterCharacteristicListViewModel _characteristicList = new();
    public List<ObservableObject> BodyComponents { get; private set; }
    
    public CharacterSheetViewModel(ICharacterSheetService characterSheetService)
    {
        BodyComponents = 
        [
            _informations,
            _characteristicList
        ];
        
        SetModel(characterSheetService.BuildCharacterSheet());
    }

    private void SetModel(CharacterSheet characterSheet)
    {
        _informations.SetModel(characterSheet.Informations);
        _characteristicList.SetModel(characterSheet.Characteristics);
    }
}