using Module.WarhammerTools.Interfaces;

namespace Module.WarhammerTools.Services;

public class CharacteristicRulesService : ICharacteristicRulesService
{
    private readonly ICharacterSheetService  _characterSheetService;
    
    public CharacteristicRulesService(ICharacterSheetService characterSheetService)
    {
        _characterSheetService = characterSheetService;
    }

    public int GetValue(string characteristicShortCut)
    {
        return _characterSheetService.GetLoadedCharacterSheet()!
            .Characteristics.First(c => c.ShortCut == characteristicShortCut).Value;
    }
    
    public int GetBonusValue(string characteristicShortCut)
    {
        return _characterSheetService.GetLoadedCharacterSheet()!
            .Characteristics.First(c => c.ShortCut == characteristicShortCut).Value / 10;
    }

}