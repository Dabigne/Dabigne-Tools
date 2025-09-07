namespace Application.Core.Interfaces.Services;

public interface IOutputService
{
    event Action<string>? MessagePushed;

    void Push(string message);
}