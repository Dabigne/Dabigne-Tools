using Module.WarhammerTools.Data;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.Services;

public class CharacterSheetService : ICharacterSheetService
{
    public CharacterSheet BuildCharacterSheet()
    {
        var sheet = new CharacterSheet
        {
            Informations = BuildInformations(),
            Characteristics = BuildCharacteristics(),
            Destiny = BuildDestiny(),
            Resilience = BuildResilience(),
            Experience = BuildExperience(),
            Movement = new CharacterMovement { Value = 0 }
        };
        return sheet;
    }
    
    private CharacterInformations BuildInformations()
    {
        return new CharacterInformations();
    }
    
    private IList<CharacterCharacteristic> BuildCharacteristics()
    {
        return [
            new CharacterCharacteristic(StringConstants.CompetenceCombatName, StringConstants.CompetenceCombatShortCut),
            new CharacterCharacteristic(StringConstants.CompetenceTirName, StringConstants.CompetenceTirShortCut),
            new CharacterCharacteristic(StringConstants.ForceName, StringConstants.ForceShortCut),
            new CharacterCharacteristic(StringConstants.EnduranceName, StringConstants.EnduranceShortCut),
            new CharacterCharacteristic(StringConstants.InitiativeName, StringConstants.InitiativeShortCut),
            new CharacterCharacteristic(StringConstants.AgiliteName, StringConstants.AgiliteShortCut),
            new CharacterCharacteristic(StringConstants.DexteriteName, StringConstants.DexteriteShortCut),
            new CharacterCharacteristic(StringConstants.IntelligenceName, StringConstants.IntelligenceShortCut),
            new CharacterCharacteristic(StringConstants.ForceMentaleName, StringConstants.ForceMentaleShortCut),
            new CharacterCharacteristic(StringConstants.SociabiliteName, StringConstants.SociabiliteShortCut),
        ];
    }

    private CharacterDestiny BuildDestiny()
    {
        return new CharacterDestiny
        {
            MaxDestinyPoints = 0,
            DestinyPoints = 0,
            MaxChancePoints = 0,
            ChancePoints = 0,
        };
    }
    
    private CharacterResilience BuildResilience()
    {
        return new CharacterResilience
        {
            Resilience = 0,
            Determination = 0,
            Motivation = 0
        };
    }

    private CharacterExperience BuildExperience()
    {
        return new CharacterExperience
        {
            Current = 0,
            Spent = 0
        };
    }
}