using Application.Core.Interfaces.Types;

namespace Application.Core.Interfaces.Services;

public interface INavigationService
{
    Type? PageType { get; }
    
    void Init();
    
    IList<INavigationItem> GetNavigationItems();
    
    INavigationViewTabItem? NavigateTo(Type pageType, string? pageParameter = null);
}