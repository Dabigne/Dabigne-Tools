namespace Application.Core.Models;

public class Session
{
    public Type? OpenedPageType { get; set; }
    
    public string? OpenedPageParameter {get; set;}
}