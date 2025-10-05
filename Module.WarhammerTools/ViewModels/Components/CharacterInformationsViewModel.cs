using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public sealed partial class CharacterInformationsViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty] 
    private string _race;
    
    [ObservableProperty]
    private string _class;

    [ObservableProperty]
    private string _career;
    
    [ObservableProperty]
    private string _careerLevel;

    [ObservableProperty]
    private string _careerSchema;

    [ObservableProperty]
    private string _status;
 
    [ObservableProperty]
    private int _age;

    [ObservableProperty]
    private int _size;

    [ObservableProperty]
    private string _hairColor;

    [ObservableProperty]
    private string _eyesColor;

    partial void OnAgeChanged(int value)
    {
    }

    public void SetModel(CharacterInformations model)
    {
        Name = model.Name;
        Race = model.Race;
        Class = model.Class;
        Career = model.Career;
        CareerLevel = model.CareerLevel;
        CareerSchema = model.CareerSchema;
        Status = model.Status;
        Age = model.Age;
        Size = model.Size;
        HairColor = model.HairColor;
        EyesColor = model.EyesColor;
    }
}