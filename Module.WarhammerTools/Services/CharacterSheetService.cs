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
            Characteristics = BuildCharacteristics()
        };
        return sheet;
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
}