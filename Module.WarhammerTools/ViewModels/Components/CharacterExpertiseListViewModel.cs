using Application.Core.Interfaces.Services;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Base;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterExpertiseListViewModel 
    : ListViewModel<CharacterExpertise, CharacterExpertiseViewModel>
{
    public CharacterExpertiseListViewModel(IInstanceProvider instanceProvider) 
        : base(instanceProvider)
    {
        Title = "COMPETENCES DE BASE";
    }
}