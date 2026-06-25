using System.Collections.ObjectModel;
using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.WarhammerTools.Interfaces;

namespace Module.WarhammerTools.ViewModels.Base;

public abstract partial class ListViewModel<TM, TVm> : ObservableObject, IListHeader where TVm : IViewModel<TM>
{
    protected readonly IInstanceProvider _instanceProvider;
    
    [ObservableProperty]
    private string _title = string.Empty;
    
    [ObservableProperty]
    private ObservableCollection<TVm> _list = [];
    
    [ObservableProperty]
    private TVm? _selectedItem;

    [RelayCommand]
    private void Add()
    {
        List.Add(_instanceProvider.GetInstance<TVm>());
    }

    [RelayCommand]
    private void Remove()
    {
        if (SelectedItem is null)
            return;
        
        List.Remove(SelectedItem);
    }
    
    protected ListViewModel(IInstanceProvider instanceProvider)
    {
        _instanceProvider = instanceProvider;
    }

    public void SetModel(IList<TM> models)
    {
        List.Clear();        
        foreach (var model in models)
        {
            var vm = _instanceProvider.GetInstance<TVm>();
            vm.SetModel(model);
            List.Add(vm);
        }
    }

    public IList<TM> GetModel()
    {
        return List.Select(i => i.GetModel()).ToList();
    }
}