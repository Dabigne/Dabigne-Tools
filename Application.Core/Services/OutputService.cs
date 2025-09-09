using Application.Core.Interfaces.Services;

namespace Application.Core.Services;

public class OutputService : IOutputService
{
    public event Action<IOutputLine>? LinePushed;
    public event Action<IOutputLine>? LineSelected;
    
    public void Push(IOutputLine line)
    {
        LinePushed?.Invoke(line);
    }

    public void SelectLine(IOutputLine line)
    {
        LineSelected?.Invoke(line);
    }
}