using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterWealthViewModel : ObservableObject, IViewModel<Models.CharacterWealth>
{
    [ObservableProperty]
    private int _copper;

    [ObservableProperty]
    private int _silver;
    
    [ObservableProperty]
    private int _gold;
    
    public CharacterWealth GetModel()
    {
        return new CharacterWealth
        {
            Copper = Copper,
            Silver = Silver,
            Gold = Gold
        };
    }

    public void SetModel(CharacterWealth model)
    {
        Copper = model.Copper;
        Silver = model.Silver;
        Gold = model.Gold;
    }
}
