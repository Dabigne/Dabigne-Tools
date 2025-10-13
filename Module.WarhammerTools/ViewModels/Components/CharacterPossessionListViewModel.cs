using Application.Core.Interfaces.Services;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Base;

namespace Module.WarhammerTools.ViewModels.Components;

public class CharacterPossessionListViewModel 
    : ListViewModel<CharacterPossession, CharacterPossessionViewModel>
{
    public CharacterPossessionListViewModel(IInstanceProvider instanceProvider) 
        : base(instanceProvider)
    {
    }
}