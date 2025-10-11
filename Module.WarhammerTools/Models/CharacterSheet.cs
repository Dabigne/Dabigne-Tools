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
    
    public CharacterAmbitions Ambitions { get; set; } = new();
    
    public CharacterGroup Group { get; set; } = new();

    public IList<CharacterArmor> Armors { get; set; } = [];
    
    public IList<CharacterPossession> Possessions { get; set; } = [];
    
    public IList<CharacterWeapon>  Weapons { get; set; } = [];
    
    public CharacterWealth Wealth { get; set; } = new();
    
    public string Psychology { get; set; } = string.Empty;
    
    public string CorruptionAndMutations  { get; set; } = string.Empty;
    
    public CharacterInjuries Injuries { get; set; } = new();
    
    public IList<CharacterSpell>  Spells { get; set; } = [];
}