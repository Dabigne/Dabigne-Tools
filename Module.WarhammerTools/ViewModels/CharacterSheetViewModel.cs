using Avalonia.Media;
using Avalonia.Media.Fonts;
using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Components;

namespace Module.WarhammerTools.ViewModels;

public sealed class CharacterSheetViewModel : ObservableObject
{
    private CharacterInformationsViewModel _informations = new();
    private CharacterCharacteristicListViewModel _characteristicList = new();
    public List<ObservableObject> BodyComponents { get; private set; } = [];
    
    public CharacterSheetViewModel()
    {
        BodyComponents = 
        [
            _informations,
            _characteristicList
        ];
        
        SetModel(new CharacterSheet());
    }

    public void SetModel(CharacterSheet characterSheet)
    {
        _informations.SetModel(characterSheet.Informations);
        _characteristicList.SetModel(characterSheet.Characteristics);
    }
}