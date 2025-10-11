using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterSkillListViewModel : ObservableObject
{
    [ObservableProperty]
    private IList<CharacterSkillViewModel> _list = [];

    [RelayCommand]
    public void Add()
    {
        var copy = List.ToList();
        copy.Add(new CharacterSkillViewModel());
        List = copy;
    }
    
    [RelayCommand]
    public void Remove() { }

    public void SetModel(IList<CharacterSkill> models)
    {
        var newList = new List<CharacterSkillViewModel>();
        foreach (var characterSkill in models)
        {
            var vm = new CharacterSkillViewModel();
            vm.SetModel(characterSkill);
            newList.Add(vm);
        }
        
        List =  newList;
    }

    public IList<CharacterSkill> GetModel()
    {
        return List.Select(i => i.GetModel()).ToList();
    }
}