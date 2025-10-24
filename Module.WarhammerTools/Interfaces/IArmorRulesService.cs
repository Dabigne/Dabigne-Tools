using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.Interfaces;

public interface IArmorRulesService
{
    IList<ArmorLocation> GetLocations();
}