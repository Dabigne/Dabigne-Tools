using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels;

namespace Module.WarhammerTools.Interfaces;

public interface ICharacterSheetService
{
    void LoadModel(CharacterSheet characterSheet);

    CharacterSheet GetLoadedCharacterSheet();

    CharacterSheet BuildCharacterSheet();

    CharacterSheet UpdateModel(CharacterSheetViewModel viewModel);
    
    void InjectModel(CharacterSheetViewModel viewModel);
}