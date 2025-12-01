using Application.Core.Interfaces.Services;
using Application.Core.Models;
using Newtonsoft.Json;

namespace Application.Core.Services;

public class SessionService : ISessionService
{
    private const string FILE_NAME = "session.json";
    
    private readonly INavigationService _navigationService;
    
    public string? Parameters { get; set; }

    public SessionService(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    
    public INavigationViewTabItem? LoadSession()
    {
        if (!File.Exists(FILE_NAME))
            return null;
        
        var reader = new JsonTextReader(new StringReader(File.ReadAllText(FILE_NAME)));
        var serializer = new JsonSerializer();
        var session = serializer.Deserialize<Session>(reader) ?? new Session();

        return session.OpenedPageType != null 
            ? _navigationService.NavigateTo(session.OpenedPageType, session.OpenedPageParameter) 
            : null;
    }

    public void SaveSession()
    {
        if (_navigationService.PageType == null)
            return;
        
        var session = new Session
        {
            OpenedPageType = _navigationService.PageType,
            OpenedPageParameter = Parameters
        };
        var writer = new JsonTextWriter(new StreamWriter(FILE_NAME));
        var serializer = new JsonSerializer();
        serializer.Serialize(writer, session, typeof(Session));
        writer.Flush();
        writer.Close();
    }
}