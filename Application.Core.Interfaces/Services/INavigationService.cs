using Application.Core.Interfaces.Types;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;

namespace Application.Core.Interfaces.Services;

public interface INavigationItem
{
    string Title { get; }

    string Icon { get; }

    Type? Type { get;}
    
    IList<INavigationItem> Children { get; }
}

public interface INavigationService
{
    Type? PageType { get; }
    
    void Init(TabControl tabControl);
    
    IList<INavigationItem> GetNavigationItems();
    
    void NavigateTo(Type pageType, string? pageParameter = null);
}