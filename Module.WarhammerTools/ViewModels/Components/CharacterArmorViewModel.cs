using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterArmorViewModel : ObservableObject, IViewModel<CharacterArmor>
{
    [ObservableProperty]
    private string _name = string.Empty;
    
    [ObservableProperty]
    private ArmorLocation _location = new();
    
    [ObservableProperty]
    private int _footprint;
    
    [ObservableProperty]
    private int _armorPoints;
    
    [ObservableProperty]
    private string _assetsAndDefaults = string.Empty;

    public IList<ArmorLocation> LocationList { get; }
    
    public CharacterArmorViewModel(IArmorRulesService armorRulesService)
    {
        LocationList =  armorRulesService.GetLocations();
    }

    public void SetModel(CharacterArmor model)
    {
        Name = model.Name;
        Location = LocationList.First(l => l.Id == model.Location);
        Footprint = model.Footprint;
        ArmorPoints = model.ArmorPoints;
        AssetsAndDefaults = model.AssetsAndDefaults;
    }

    public CharacterArmor GetModel()
    {
        return new CharacterArmor
        {
            Name = Name,
            Location = Location.Id,
            Footprint = Footprint,
            ArmorPoints = ArmorPoints,
            AssetsAndDefaults = AssetsAndDefaults
        };
    }
}