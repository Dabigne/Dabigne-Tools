namespace Module.WarhammerTools.Interfaces;

public interface IClutterRulesService
{
    int GetArmorClutter();
    
    int GetWeaponClutter();
    
    int GetPossessionClutter();
    
    int GetMaxClutter();
    
    int GetTotalClutter();
}