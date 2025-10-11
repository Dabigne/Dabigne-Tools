using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.WarhammerTools.Interfaces;

namespace Module.WarhammerTools.ViewModels.Base;

public abstract partial class ListViewModel<Tm, Tvm> : ObservableObject where Tvm : IViewModel<Tm>, new()
{
    [ObservableProperty]
    private IList<Tvm> _list = [];
    
    [ObservableProperty]
    private Tvm? _selectedItem;

    [RelayCommand]
    public void Add()
    {
        var copy = List.ToList();
        copy.Add(new Tvm());
        List = copy;
    }

    [RelayCommand]
    public void Remove()
    {
        if (SelectedItem is null)
            return;
        
        List.Remove(SelectedItem);
        List = List.ToList();
    }

    public void SetModel(IList<Tm> models)
    {
        var newList = new List<Tvm>();
        foreach (var model in models)
        {
            var vm = new Tvm();
            vm.SetModel(model);
            newList.Add(vm);
        }
        
        List = newList;
    }

    public IList<Tm> GetModel()
    {
        return List.Select(i => i.GetModel()).ToList();
    }
}