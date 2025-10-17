using Module.WarhammerTools.Interfaces;

namespace Module.WarhammerTools.Services;

public class ClutterRulesService : IClutterRulesService
{
    private readonly ICharacteristicRulesService _characteristicRulesService;
    private readonly ICharacterSheetService _characterSheetService;
    
    public ClutterRulesService(
        ICharacteristicRulesService characteristicRulesService,
        ICharacterSheetService characterSheetService)
    {
        _characteristicRulesService = characteristicRulesService;
        _characterSheetService = characterSheetService;
    }
    
    public int GetArmorClutter()
    {
        return _characterSheetService.GetLoadedCharacterSheet().Armors.Sum(a => a.Footprint);
    }

    public int GetWeaponClutter()
    {
        return _characterSheetService.GetLoadedCharacterSheet().Weapons.Sum(a => a.Footprint);
    }

    public int GetPossessionClutter()
    {
        return _characterSheetService.GetLoadedCharacterSheet().Possessions.Sum(a => a.Footprint);
    }

    public int GetMaxClutter()
    {
        return 10 * _characteristicRulesService.GetValue("F");
    }

    public int GetTotalClutter()
    {
        return GetArmorClutter() + GetWeaponClutter() + GetPossessionClutter();
    }
}