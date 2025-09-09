using Application.Core.Interfaces.Services;
using Avalonia.Media;

namespace Application.Core.Models;

public class OutputLine : IOutputLine
{
    public string Content { get; }
    public bool IsSelectable { get; }
    
    public Color Color { get; }
    
    public OutputLine(
        string content, 
        bool isSelectable = false, 
        Color color = default)
    {
        Content = content;
        IsSelectable = isSelectable;
        Color = color == default ? Colors.White : color;
    }
}