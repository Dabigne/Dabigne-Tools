using Application.Core.Interfaces.Services;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Base;

namespace Module.WarhammerTools.ViewModels.Components;

public class CharacterWeaponListViewModel 
    : ListViewModel<CharacterWeapon, CharacterWeaponViewModel>
{
    public CharacterWeaponListViewModel(IInstanceProvider instanceProvider) 
        : base(instanceProvider)
    {
    }
}