using Application.Core.Interfaces.Services;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Base;

namespace Module.WarhammerTools.ViewModels.Components;

public class CharacterSpellListViewModel 
    : ListViewModel<CharacterSpell, CharacterSpellViewModel>
{
    public CharacterSpellListViewModel(IInstanceProvider instanceProvider) 
        : base(instanceProvider)
    {
    }
}