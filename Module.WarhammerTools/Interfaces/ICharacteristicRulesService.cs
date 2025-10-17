namespace Module.WarhammerTools.Interfaces;

public interface ICharacteristicRulesService
{
    int GetValue(string characteristicShortCut);
    
    int GetBonusValue(string characteristicShortCut);

}