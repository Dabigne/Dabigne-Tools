using Application.Core.Interfaces.Services;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Base;

namespace Module.WarhammerTools.ViewModels.Components;

public class CharacterArmorListViewModel 
    : ListViewModel<CharacterArmor, CharacterArmorViewModel>
{
    public CharacterArmorListViewModel(IInstanceProvider instanceProvider) 
        : base(instanceProvider)
    {
        Title = "ARMURES";
    }
}