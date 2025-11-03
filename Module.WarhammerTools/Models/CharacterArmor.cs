namespace Module.WarhammerTools.Models;

public class CharacterArmor
{
    public string Name { get; set; } = string.Empty;
    
    public ArmorLocationId Location {get; set;}
    
    public int Footprint { get; set; }
    
    public int ArmorPoints { get; set; }
    
    public string AssetsAndDefaults { get; set; } = string.Empty;
}