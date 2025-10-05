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
            Movement = new CharacterMovement { Value = 6 }
        };
        return sheet;
    }
    
    private CharacterInformations BuildInformations()
    {
        return new CharacterInformations
        {
            Name  = "Shorleck Halmes",
            Race  = "Humaine",
            Class  = "Citadin",
            Career  = string.Empty,
            CareerLevel = "Limier",
            CareerSchema = "Enquéteur",
            Status = "Argent 1",
            Age  = 17,
            Size = 167,
            HairColor = "Noir",
            EyesColor = "Gris pale",
        };
    }
    
    private IList<CharacterCharacteristic> BuildCharacteristics()
    {
        return [
            new CharacterCharacteristic(StringConstants.CompetenceCombatName, StringConstants.CompetenceCombatShortCut, 10, 1),
            new CharacterCharacteristic(StringConstants.CompetenceTirName, StringConstants.CompetenceTirShortCut, 10, 1),
            new CharacterCharacteristic(StringConstants.ForceName, StringConstants.ForceShortCut, 10, 1),
            new CharacterCharacteristic(StringConstants.EnduranceName, StringConstants.EnduranceShortCut, 10, 1),
            new CharacterCharacteristic(StringConstants.InitiativeName, StringConstants.InitiativeShortCut, 10, 1),
            new CharacterCharacteristic(StringConstants.AgiliteName, StringConstants.AgiliteShortCut, 10, 1),
            new CharacterCharacteristic(StringConstants.DexteriteName, StringConstants.DexteriteShortCut, 10, 1),
            new CharacterCharacteristic(StringConstants.IntelligenceName, StringConstants.IntelligenceShortCut, 10, 1),
            new CharacterCharacteristic(StringConstants.ForceMentaleName, StringConstants.ForceMentaleShortCut, 10, 1),
            new CharacterCharacteristic(StringConstants.SociabiliteName, StringConstants.SociabiliteShortCut, 10, 1),
        ];
    }

    private CharacterDestiny BuildDestiny()
    {
        return new CharacterDestiny
        {
            MaxDestinyPoints = 3,
            DestinyPoints = 3,
            MaxChancePoints = 3,
            ChancePoints = 3,
        };
    }
    
    private CharacterResilience BuildResilience()
    {
        return new CharacterResilience
        {
            Resilience = 3,
            Determination = 3,
            Motivation = 0
        };
    }

    private CharacterExperience BuildExperience()
    {
        return new CharacterExperience
        {
            Current = 35,
            Spent = 515
        };
    }
}