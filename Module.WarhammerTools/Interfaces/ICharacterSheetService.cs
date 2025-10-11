using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels;

namespace Module.WarhammerTools.Interfaces;

public interface ICharacterSheetService
{
    CharacterSheet BuildCharacterSheet();

    CharacterSheet GetModel(CharacterSheetViewModel viewModel);
    
    void SetModel(CharacterSheetViewModel viewModel, CharacterSheet characterSheet);
}