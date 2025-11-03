namespace Application.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class NavigatableAttribute : Attribute
{
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    
    public string Folder {get; set; }=  string.Empty;
}