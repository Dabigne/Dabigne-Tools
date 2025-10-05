namespace Module.WarhammerTools.Models;

public class CharacterExpertise
{
    public string Name { get; set; }
    
    public CharacterCharacteristic Characteristic { get; private set; }
    
    public int Improvement {get; set; }
    
    public int Value => Characteristic.Value + Improvement;

    public CharacterExpertise(CharacterCharacteristic characteristic)
    {
        Characteristic = characteristic;
    }
}