using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.Interfaces;

public interface ICharacterSheetFileService
{
    CharacterSheet Load(string fileName);
    void Save(CharacterSheet characterSheet, string fileName);
}