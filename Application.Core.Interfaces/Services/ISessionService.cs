namespace Application.Core.Interfaces.Services;

public interface ISessionService
{
    string? Parameters {get; set;}
    
    void LoadSession();
    
    void SaveSession();
}