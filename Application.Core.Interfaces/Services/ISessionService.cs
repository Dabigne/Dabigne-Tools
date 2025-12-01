namespace Application.Core.Interfaces.Services;

public interface ISessionService
{
    string? Parameters {get; set;}
    
    INavigationViewTabItem? LoadSession();
    
    void SaveSession();
}