using CommunityToolkit.Mvvm.ComponentModel;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterCorruptionAndMutationViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _value;
    
    public void SetModel(string value) => _value = value; 
    
    public string GetModel() => _value;
}