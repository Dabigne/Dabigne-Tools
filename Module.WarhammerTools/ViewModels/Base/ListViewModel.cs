using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.WarhammerTools.Interfaces;

namespace Module.WarhammerTools.ViewModels.Base;

public abstract partial class ListViewModel<Tm, Tvm> : ObservableObject where Tvm : IViewModel<Tm>, new()
{
    [ObservableProperty]
    private ObservableCollection<Tvm> _list = [];
    
    [ObservableProperty]
    private Tvm? _selectedItem;

    [RelayCommand]
    public void Add()
    {
        List.Add(new Tvm());
    }

    [RelayCommand]
    public void Remove()
    {
        if (SelectedItem is null)
            return;
        
        List.Remove(SelectedItem);
    }

    public void SetModel(IList<Tm> models)
    {
        var newList = new List<Tvm>();
        foreach (var model in models)
        {
            var vm = new Tvm();
            vm.SetModel(model);
            List.Add(vm);
        }
    }

    public IList<Tm> GetModel()
    {
        return List.Select(i => i.GetModel()).ToList();
    }
}