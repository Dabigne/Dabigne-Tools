using CommunityToolkit.Mvvm.ComponentModel;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterCorruptionAndMutationViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _value = string.Empty;
    
    public void SetModel(string value) => Value = value; 
    
    public string GetModel() => Value;
}