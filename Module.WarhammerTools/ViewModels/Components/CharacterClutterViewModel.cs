using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;

namespace Module.WarhammerTools.ViewModels.Components;

public class CharacterClutterViewModel(IClutterRulesService clutterRulesService) 
	: ObservableObject
{
    public int ArmorClutter => clutterRulesService.GetArmorClutter();

    public int WeaponClutter => clutterRulesService.GetWeaponClutter();
    
    public int PossessionsClutter => clutterRulesService.GetPossessionClutter();	
    
    public int TotalClutter => clutterRulesService.GetTotalClutter();
    
    public int MaxClutter => clutterRulesService.GetMaxClutter();
    
    public void Refresh()
	{
		OnPropertyChanged(nameof(ArmorClutter));
		OnPropertyChanged(nameof(WeaponClutter));
		OnPropertyChanged(nameof(PossessionsClutter));
		OnPropertyChanged(nameof(TotalClutter));
		OnPropertyChanged(nameof(MaxClutter));
	}
}