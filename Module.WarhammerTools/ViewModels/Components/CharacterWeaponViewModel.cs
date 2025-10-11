using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterWeaponViewModel : ObservableObject, IViewModel<CharacterWeapon>
{
    [ObservableProperty]
    private string _name = string.Empty;
    
    [ObservableProperty]
    private string _group = string.Empty;

    [ObservableProperty]
    private int _footprint = 0;
    
    [ObservableProperty]
    private string _range = string.Empty;
    
    [ObservableProperty]
    private string _damages = string.Empty;
    
    [ObservableProperty]
    private string _assetsAndDefaults = String.Empty;
    
    public CharacterWeapon GetModel()
    {
        return new CharacterWeapon()
        {
            Name = Name,
            Group = Group,
            Footprint = Footprint,
            Range = Range,
            Damages = Damages,
            AssetsAndDefaults = AssetsAndDefaults,
        };
    }

    public void SetModel(CharacterWeapon model)
    {
        Name = model.Name;
        Group = model.Group;
        Footprint = model.Footprint;
        Range = model.Range;
        Damages = model.Damages;
        AssetsAndDefaults = model.AssetsAndDefaults;
    }
}