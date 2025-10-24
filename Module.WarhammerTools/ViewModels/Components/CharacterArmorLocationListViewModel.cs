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
        
        _locations.Add(new CharacterArmorLocationViewModel{HitScore = "01-09", Title = "Tête", Value = 0});
        _locations.Add(new CharacterArmorLocationViewModel{HitScore = "10-24", Title = "Bras gauche", Value = 0});
        _locations.Add(new CharacterArmorLocationViewModel{HitScore = "25-44", Title = "Bras droit", Value = 0});
        _locations.Add(new CharacterArmorLocationViewModel{HitScore = "45-79", Title = "Corps", Value = 0});
        _locations.Add(new CharacterArmorLocationViewModel{HitScore = "90-00", Title = "Jambe droite", Value = 0});
        _locations.Add(new CharacterArmorLocationViewModel{HitScore = "80-89", Title = "Jambe gauche", Value = 0});
    }
}