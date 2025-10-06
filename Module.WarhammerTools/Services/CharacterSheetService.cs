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
            Movement = new CharacterMovement { Value = 0 },
            Expertises = BuildExpertise(),
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

    private IList<CharacterExpertise> BuildExpertise()
    {
        return 
        [
            new CharacterExpertise { Name = ExpertiseNames.Art, Characteristic = StringConstants.DexteriteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Athletisme, Characteristic = StringConstants.AgiliteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Calme, Characteristic = StringConstants.ForceMentaleShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Charme, Characteristic = StringConstants.SociabiliteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Chevaucher, Characteristic = StringConstants.AgiliteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Commandement, Characteristic = StringConstants.SociabiliteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.ConduiteAttelage, Characteristic = StringConstants.AgiliteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.CorpsACorpsBase, Characteristic = StringConstants.CompetenceCombatShortCut},
            new CharacterExpertise { Name = ExpertiseNames.CorpsACorps, Characteristic = StringConstants.CompetenceCombatShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Discretion, Characteristic = StringConstants.AgiliteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Divertissement, Characteristic = StringConstants.SociabiliteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.EmpriseAnimaux, Characteristic = StringConstants.ForceMentaleShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Escalade, Characteristic = StringConstants.ForceShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Esquive, Characteristic = StringConstants.AgiliteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Intimidation, Characteristic = StringConstants.ForceShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Intuition, Characteristic = StringConstants.InitiativeShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Marchandage, Characteristic = StringConstants.SociabiliteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Navigation, Characteristic = StringConstants.InitiativeShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Pari, Characteristic = StringConstants.AgiliteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Perception, Characteristic = StringConstants.IntelligenceShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Ragot, Characteristic = StringConstants.SociabiliteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Ramer, Characteristic = StringConstants.ForceShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Resistance, Characteristic = StringConstants.EnduranceShortCut},
            new CharacterExpertise { Name = ExpertiseNames.ResistanceAlcool, Characteristic = StringConstants.EnduranceShortCut},
            new CharacterExpertise { Name = ExpertiseNames.Subornation, Characteristic = StringConstants.SociabiliteShortCut},
            new CharacterExpertise { Name = ExpertiseNames.SurvieExterieur, Characteristic = StringConstants.IntelligenceShortCut},
        ];
    }
}