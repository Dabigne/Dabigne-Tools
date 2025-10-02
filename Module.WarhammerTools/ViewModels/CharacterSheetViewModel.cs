using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.ViewModels.Components;

namespace Module.WarhammerTools.ViewModels;

public sealed class CharacterSheetViewModel : ObservableObject
{
    public List<ObservableObject> BodyComponents { get; private set; } = [];
    
    public CharacterSheetViewModel()
    {
        BodyComponents = 
        [
            new CharacterInformationsViewModel(),
            new CharacterCharacteristicListViewModel()
        ];
    }
}