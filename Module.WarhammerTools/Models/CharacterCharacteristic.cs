namespace Module.WarhammerTools.Models;

public class CharacterCharacteristic
{
    public string Name { get; set; }
    
    public string ShortCut { get; set; }
    
    public int InitialValue { get; set; }

    public int Improvments { get; set; }
    
    public int Value => InitialValue + Improvments;
    
    public CharacterCharacteristic(
        string name,
        string shortCut, 
        int initialValue, 
        int improvments)
    {
        Name = name;
        ShortCut = shortCut;
        InitialValue = initialValue;
        Improvments = improvments;
    }
}