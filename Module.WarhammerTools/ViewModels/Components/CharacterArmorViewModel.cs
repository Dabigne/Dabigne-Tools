using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterArmorViewModel : ObservableObject, IViewModel<CharacterArmor>
{
    [ObservableProperty]
    private string _name = string.Empty;
    
    [ObservableProperty]
    private string _location = string.Empty;
    
    [ObservableProperty]
    private int _footprint = 0;
    
    [ObservableProperty]
    private int _armorPoints = 0;
    
    [ObservableProperty]
    private string _assetsAndDefaults = string.Empty;

    public void SetModel(CharacterArmor model)
    {
        Name = model.Name;
        Location = model.Location;
        Footprint = model.Footprint;
        ArmorPoints = model.ArmorPoints;
        AssetsAndDefaults = model.AssetsAndDefaults;
    }

    public CharacterArmor GetModel()
    {
        return new CharacterArmor
        {
            Name = Name,
            Location = Location,
            Footprint = Footprint,
            ArmorPoints = ArmorPoints,
            AssetsAndDefaults = AssetsAndDefaults
        };
    }
}