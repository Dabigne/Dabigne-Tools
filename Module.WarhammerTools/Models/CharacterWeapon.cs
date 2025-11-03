namespace Module.WarhammerTools.Models;

public class CharacterWeapon
{
    public string Name { get; set; } = string.Empty;
    
    public string Group { get; set; } = string.Empty;

    public int Footprint { get; set; }
    
    public string Range {get; set;} = string.Empty;
    
    public string Damages {get; set;} = string.Empty;
    
    public string AssetsAndDefaults {get; set;} = string.Empty;
}