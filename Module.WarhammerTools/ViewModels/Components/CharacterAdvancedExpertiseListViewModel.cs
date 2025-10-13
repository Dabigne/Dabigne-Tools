using Application.Core.Interfaces.Services;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Base;

namespace Module.WarhammerTools.ViewModels.Components;

public class CharacterAdvancedExpertiseListViewModel 
    : ListViewModel<CharacterExpertise, CharacterExpertiseViewModel>
{
    public CharacterAdvancedExpertiseListViewModel(IInstanceProvider instanceProvider) 
        : base(instanceProvider)
    {
    }
}