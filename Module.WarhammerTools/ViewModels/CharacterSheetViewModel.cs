using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.ViewModels.Components;

namespace Module.WarhammerTools.ViewModels;

public class CharacterSheetViewModel : ObservableObject
{
    public CharacterInformationsViewModel Informations { get; }

    public CharacterSheetViewModel()
    {
        Informations = new CharacterInformationsViewModel();
    }
}