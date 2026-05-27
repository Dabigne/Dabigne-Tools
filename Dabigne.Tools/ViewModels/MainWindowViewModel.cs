using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Application.Core.Interfaces.Services;
using Application.Core.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Dabigne.Tools.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly ISessionService _sessionService;

    public IList<INavigationItem> NavigationItems { get; }

    public ObservableCollection<INavigationViewTabItem> TabItems { get; } = [];
    
    public OutputViewModel Output  { get; }
    
    [ObservableProperty]
    private INavigationItem? _selectedItem;
    
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
    
    public MainWindowViewModel(
        INavigationService navigationService, 
        IOutputService outputService,
        ISessionService sessionService)
    {
        _navigationService = navigationService;
        _sessionService = sessionService;

        _navigationService.Init();
        
        Output = new OutputViewModel(outputService);
        NavigationItems = _navigationService.GetNavigationItems().ToList();
        
        var item = _sessionService.LoadSession();
        AddTabItem(item);
        
        SelectedItem = _navigationService.PageType != null 
            ? NavigationItems.FirstOrDefault(x => x.Type == _navigationService.PageType) 
            : NavigationItems.First();
    }

    partial void OnSelectedItemChanged(INavigationItem? value)
    {
        if(value?.Type == null || value.Type == _navigationService.PageType)
            return;
        
        var item = _navigationService.NavigateTo(value.Type);
        AddTabItem(item);
    }

    private void AddTabItem(INavigationViewTabItem? tabItem)
    {
        if (tabItem == null)
            return;
        
        tabItem.CloseCommand = new RelayCommand<INavigationViewTabItem>(CloseTabItem);
        TabItems.Add(tabItem);
    }
    
    private void CloseTabItem(INavigationViewTabItem? tabItem)
    {
        if (tabItem == null)
            return;
        
        TabItems.Remove(tabItem);
    }

    public void Close()
    {
        _sessionService.SaveSession();
        TabItems.Clear();
    }
}