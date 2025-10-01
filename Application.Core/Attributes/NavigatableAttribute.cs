namespace Application.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class NavigatableAttribute : Attribute
{
    public string Title { get; set; }
    public string Icon { get; set; }
    
    public string Folder {get; set; }
}