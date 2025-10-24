using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterArmorLocationListViewModel : ObservableObject
{
    private readonly IArmorRulesService _armorRulesService;

    [ObservableProperty] 
    private IList<CharacterArmorLocationViewModel> _locations = [];

    public CharacterArmorLocationListViewModel(IArmorRulesService armorRulesService)
    {
        _armorRulesService = armorRulesService;
    }

    public void SetModel()
    {
        var locations = _armorRulesService.GetLocations();
        var values = _armorRulesService.GetValues();

        var newLocations = 
        locations.Select(armorLocation => new CharacterArmorLocationViewModel
        {
            Title = armorLocation.Name, 
            HitScore = $"{armorLocation.MinHitInformation}-{armorLocation.MaxHitInformation}", 
            Value = values[armorLocation.Id]
        }).ToList();

        Locations = newLocations;
    }
}