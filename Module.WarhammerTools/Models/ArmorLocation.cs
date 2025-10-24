namespace Module.WarhammerTools.Models;

public enum ArmorLocationId
{
    Head = 0,
    LeftArm = 1,
    RightArm = 2,
    Body = 3,
    RightLeg = 4,
    LeftLeg = 5,
    Shield = 6
}

public class ArmorLocation
{
    public ArmorLocationId Id { get; set; }
    
    public string Name { get; set; }
    
    public int MinHitInformation { get; set; }

    public int MaxHitInformation { get; set; }
}