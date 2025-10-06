using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterExpertiseViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _name;
    
    [ObservableProperty]
    private string _characteristic;
    
    [ObservableProperty]
    private int _improvement;

    [ObservableProperty] 
    private int _value;

    public void SetModel(CharacterExpertise model)
    {
        Name = model.Name;
        Characteristic = model.Characteristic;
        Improvement = model.Improvement;
    }

    public CharacterExpertise GetModel()
    {
        return new CharacterExpertise
        {
            Name = Name,
            Characteristic = Characteristic,
            Improvement = Improvement
        };
    }
}