using Module.WarhammerTools.Data;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels;

namespace Module.WarhammerTools.Services;

public class CharacterSheetService : ICharacterSheetService
{
    private CharacterSheet _loadedCharacterSheet;

    public CharacterSheetService()
    {
        BuildCharacterSheet();
    }
    
    public void LoadModel(CharacterSheet characterSheet)
    {
        _loadedCharacterSheet = characterSheet;
    }

    public CharacterSheet GetLoadedCharacterSheet()
    {
        return _loadedCharacterSheet;
    }
    
    public CharacterSheet BuildCharacterSheet()
    {
        _loadedCharacterSheet = new CharacterSheet
        {
            Informations = BuildInformations(),
            Characteristics = BuildCharacteristics(),
            Destiny = BuildDestiny(),
            Resilience = BuildResilience(),
            Experience = BuildExperience(),
            Movement = new CharacterMovement { Value = 0 },
            Expertises = BuildExpertise(),
        };
        return _loadedCharacterSheet;
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
    
    public CharacterSheet UpdateModel(CharacterSheetViewModel viewModel)
    {
        var model = new CharacterSheet
        {
            Informations = viewModel.Informations.GetModel(),
            Characteristics = viewModel.CharacteristicList.GetModel(),
            Destiny = viewModel.Destiny.GetModel(),
            Resilience = viewModel.Resilience.GetModel(),
            Experience = viewModel.Experience.GetModel(),
            Movement = viewModel.Movement.GetModel(),
            Expertises = viewModel.GetExpertises(),
            AdvancedExpertises = viewModel.AdvancedExpertiseList.GetModel(),
            Skills = viewModel.SkillList.GetModel(),
            Ambitions = viewModel.Ambitions.GetModel(),
            Group = viewModel.Group.GetModel(),
            Armors = viewModel.Armors.GetModel(),
            Possessions = viewModel.Possessions.GetModel(),
            Weapons = viewModel.Weapons.GetModel(),
            Psychology = viewModel.Psychology.GetModel(),
            CorruptionAndMutations = viewModel.CorruptionAndMutations.GetModel(),
        };

        _loadedCharacterSheet = model;
        return _loadedCharacterSheet;
    }

    public void InjectModel(CharacterSheetViewModel viewModel)
    {
        if (_loadedCharacterSheet == null)
            return;
        
        viewModel.Informations.SetModel(_loadedCharacterSheet.Informations);
        viewModel.CharacteristicList.SetModel(_loadedCharacterSheet.Characteristics);
        viewModel.Destiny.SetModel(_loadedCharacterSheet.Destiny);
        viewModel.Resilience.SetModel(_loadedCharacterSheet.Resilience);
        viewModel.Experience.SetModel(_loadedCharacterSheet.Experience);
        viewModel.Movement.SetModel(_loadedCharacterSheet.Movement);
        viewModel.SetExpertises(_loadedCharacterSheet.Expertises);
        viewModel.AdvancedExpertiseList.SetModel(_loadedCharacterSheet.AdvancedExpertises);
        viewModel.SkillList.SetModel(_loadedCharacterSheet.Skills);
        viewModel.Ambitions.SetModel(_loadedCharacterSheet.Ambitions);
        viewModel.Group.SetModel(_loadedCharacterSheet.Group);
        viewModel.Armors.SetModel(_loadedCharacterSheet.Armors);
        viewModel.Possessions.SetModel(_loadedCharacterSheet.Possessions);
        viewModel.Weapons.SetModel(_loadedCharacterSheet.Weapons);
        viewModel.Psychology.SetModel(_loadedCharacterSheet.Psychology);
        viewModel.CorruptionAndMutations.SetModel(_loadedCharacterSheet.CorruptionAndMutations);
    }
}