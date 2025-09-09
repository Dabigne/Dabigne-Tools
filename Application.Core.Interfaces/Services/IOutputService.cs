using Avalonia.Media;

namespace Application.Core.Interfaces.Services;

public interface IOutputLine
{
    public string Content { get; }
    
    public bool IsSelectable { get; }

    public Color Color { get; }
}

public interface IOutputService
{
    event Action<IOutputLine>? LinePushed;

    public event Action<IOutputLine>? LineSelected;

    void Push(IOutputLine line);

    void SelectLine(IOutputLine line);  
}