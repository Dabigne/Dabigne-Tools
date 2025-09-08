using Application.Core.Interfaces.Services;

namespace Application.Core.Services;

public class OutputService : IOutputService
{
    public event Action<string>? LinePushed;
    public event Action<string>? LineSelected;
    
    public void Push(string line)
    {
        LinePushed?.Invoke(line);
    }

    public void SelectLine(string line)
    {
        LineSelected?.Invoke(line);
    }
}