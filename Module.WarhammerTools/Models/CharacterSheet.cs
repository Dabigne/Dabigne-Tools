using Module.WarhammerTools.Data;

namespace Module.WarhammerTools.Models;

public class CharacterSheet
{
    public CharacterInformations Informations { get; set; } = new();

    public IList<CharacterCharacteristic> Characteristics { get; set; } = [];
    
    public CharacterSheet()
    {
        Characteristics =
        [
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