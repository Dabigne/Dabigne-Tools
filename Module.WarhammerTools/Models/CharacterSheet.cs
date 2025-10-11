using Module.WarhammerTools.Data;

namespace Module.WarhammerTools.Models;

public class CharacterSheet
{
    public CharacterInformations Informations { get; set; } = new();

    public IList<CharacterCharacteristic> Characteristics { get; set; } = [];
    
    public CharacterDestiny Destiny { get; set; } = new();
    
    public CharacterResilience Resilience { get; set; } = new();
    
    public CharacterExperience Experience { get; set; } = new();
    
    public CharacterMovement Movement { get; set; } = new();

    public IList<CharacterExpertise> Expertises { get; set; } = [];

    public IList<CharacterExpertise> AdvancedExpertises { get; set; } = [];

    public IList<CharacterSkill> Skills { get; set; } = [];
}