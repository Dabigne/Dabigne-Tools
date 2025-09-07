using Application.Core.Interfaces.Services;

namespace Application.Core.Services;

public class OutputService : IOutputService
{
    public event Action<string>? MessagePushed;
        
    public void Push(string message)
    {
        MessagePushed?.Invoke(message);
    }
}