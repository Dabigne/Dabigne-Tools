using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.Services;

public class ArmorRulesService : IArmorRulesService
{
    private readonly ICharacterSheetService _characterSheetService;
    
    private readonly IList<ArmorLocation> _locations = new List<ArmorLocation>()
    {
        new() {Id = ArmorLocationId.Head, Name = "Tête", MinHitInformation = 1, MaxHitInformation = 9},
        new() {Id = ArmorLocationId.LeftArm, Name = "Bras gauche", MinHitInformation = 10, MaxHitInformation = 24},
        new() {Id = ArmorLocationId.RightArm, Name = "Bras droit", MinHitInformation = 25, MaxHitInformation = 44},
        new() {Id = ArmorLocationId.Body, Name = "Corps", MinHitInformation = 45, MaxHitInformation = 79},
        new() {Id = ArmorLocationId.RightLeg, Name = "Jambe droite", MinHitInformation = 90, MaxHitInformation = 100},
        new() {Id = ArmorLocationId.LeftLeg, Name = "Jambe gauche", MinHitInformation = 80, MaxHitInformation = 89},
        new() {Id = ArmorLocationId.Shield, Name = "Bouclier"},
    };

    public ArmorRulesService(ICharacterSheetService characterSheetService)
    {
        _characterSheetService = characterSheetService;
    }
    
    public IList<ArmorLocation> GetLocations()
    {
        return _locations;
    }

    public Dictionary<ArmorLocationId, int> GetValues()
    {
        var result = new Dictionary<ArmorLocationId, int>
        {
            { ArmorLocationId.Head, 0 },
            { ArmorLocationId.LeftArm, 0 },
            { ArmorLocationId.RightArm, 0 },
            { ArmorLocationId.Body, 0 },
            { ArmorLocationId.RightLeg, 0 },
            { ArmorLocationId.LeftLeg, 0 },
            { ArmorLocationId.Shield, 0 }
        };

        var armors = _characterSheetService.GetLoadedCharacterSheet()!.Armors;
        foreach (var armor in armors)
        {
            result[armor.Location] += armor.ArmorPoints;
        }
        
        return result;
    }
}