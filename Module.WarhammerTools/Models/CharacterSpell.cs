namespace Module.WarhammerTools.Models;

public class CharacterSpell
{
    public string Name { get; set; } = string.Empty;
    
    public int Level { get; set; }
    
    public string Range { get; set; } = string.Empty;
    
    public string Target { get; set; } = string.Empty;
    
    public string Duration { get; set; } = string.Empty;
    
    public string Effects { get; set; } = string.Empty;
}