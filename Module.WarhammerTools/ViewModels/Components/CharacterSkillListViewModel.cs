using Application.Core.Interfaces.Services;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Base;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterSkillListViewModel 
    : ListViewModel<CharacterSkill, CharacterSkillViewModel>
{
    public CharacterSkillListViewModel(IInstanceProvider instanceProvider) 
        : base(instanceProvider)
    {
        Title = "TALENTS";
    }
}