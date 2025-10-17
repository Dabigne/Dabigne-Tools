using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;

namespace Module.WarhammerTools.ViewModels.Components;

public class CharacterClutterViewModel : ObservableObject
{
    private IClutterRulesService _rulesService;
    
    public int ArmorClutter => _rulesService.GetArmorClutter();

    public int WeaponClutter => _rulesService.GetWeaponClutter();
    
    public int PossessionsClutter => _rulesService.GetPossessionClutter();
    
    public int TotalClutter => _rulesService.GetTotalClutter();
    
    public int MaxClutter => _rulesService.GetMaxClutter();
    
    public CharacterClutterViewModel(IClutterRulesService rulesService)
    {
        _rulesService = rulesService;
    }
}