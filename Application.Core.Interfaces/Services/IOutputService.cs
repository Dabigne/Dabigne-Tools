namespace Application.Core.Interfaces.Services;

public interface IOutputService
{
    event Action<string>? LinePushed;

    void Push(string line);
}