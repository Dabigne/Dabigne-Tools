using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.Services;

public class ArmorRulesService : IArmorRulesService
{
    private readonly IList<ArmorLocation> _locations = new List<ArmorLocation>()
    {
        new ArmorLocation{Id = ArmorLocationId.Head, Name = "Tête"},
        new ArmorLocation{Id = ArmorLocationId.LeftArm, Name = "Bras gauche"},
        new ArmorLocation{Id = ArmorLocationId.RightArm, Name = "Bras droit"},
        new ArmorLocation{Id = ArmorLocationId.Body, Name = "Corps"},
        new ArmorLocation{Id = ArmorLocationId.RightLeg, Name = "Jambe droite"},
        new ArmorLocation{Id = ArmorLocationId.LeftLeg, Name = "Jambe gauche"},
        new ArmorLocation{Id = ArmorLocationId.Shield, Name = "Bouclier"},
    };
    
    public IList<ArmorLocation> GetLocations()
    {
        return _locations;
    }
}