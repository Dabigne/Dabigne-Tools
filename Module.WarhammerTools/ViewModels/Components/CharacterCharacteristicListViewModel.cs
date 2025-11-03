using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterCharacteristicListViewModel : ObservableObject
{
    [ObservableProperty] 
    private IList<CharacterCharacteristicViewModel>? _list;
    
    public void SetModel(IList<CharacterCharacteristic> models)
    {
        var newList = new List<CharacterCharacteristicViewModel>();
        foreach (var model in models)
        {
            var vm = new CharacterCharacteristicViewModel();
            vm.SetModel(model);
            newList.Add(vm);
        }

        List = newList;
    }

    public IList<CharacterCharacteristic> GetModel()
    {
        return List!.Select(i => i.GetModel()).ToList();
    }
}