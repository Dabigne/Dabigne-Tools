using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterAdvancedExpertiseListViewModel : ObservableObject
{
    [ObservableProperty]
    private IList<CharacterExpertiseViewModel> _list = [];
    
    [ObservableProperty]
    private CharacterExpertiseViewModel? _selectedItem;

    [RelayCommand]
    public void AddExpertise()
    {
        var copy = List.ToList();
        copy.Add(new CharacterExpertiseViewModel());
        List = copy;
    }

    [RelayCommand]
    public void RemoveExpertise()
    {
        if (SelectedItem is null)
            return;
        
        List.Remove(SelectedItem);
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(List)));
    }

    public void SetModel(
        IList<CharacterExpertise> expertises, 
        IList<CharacterCharacteristic> characteristics)
    {
        var newList = new List<CharacterExpertiseViewModel>();
        foreach (var modelItem in expertises)
        {
            var vm = new CharacterExpertiseViewModel();
            vm.SetModel(modelItem, characteristics.FirstOrDefault(c => c.ShortCut == modelItem.Characteristic));
            newList.Add(vm);
        }
        
        List = newList;
    }

    public IList<CharacterExpertise> GetModel()
    {
        return List.Select(i => i.GetModel()).ToList();
    }
}