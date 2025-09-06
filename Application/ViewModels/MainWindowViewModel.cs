using System.Collections.Generic;
using System.Linq;
using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Application.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public IList<INavigationItem> NavigationItems { get; } = new List<INavigationItem>();
    
    [ObservableProperty]
    private INavigationItem _selectedItem;
    
    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty] 
    private string _paneButtonContent = "<";

    [RelayCommand]
    private void TogglePane()
    {
        IsPaneOpen = !IsPaneOpen;
        PaneButtonContent = IsPaneOpen ? "<" : ">";
    }
    
    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        NavigationItems = _navigationService.GetNavigationItems().ToList();
        SelectedItem = NavigationItems.First();
    }

    partial void OnSelectedItemChanged(INavigationItem navigationItem)
    {
        if (SelectedItem == null)
            return;
        
        _navigationService.NavigateTo(SelectedItem.Type);
    }
}