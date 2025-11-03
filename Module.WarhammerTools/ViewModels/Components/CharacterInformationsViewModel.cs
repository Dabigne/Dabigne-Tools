using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public sealed partial class CharacterInformationsViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty] 
    private string _race  = string.Empty;
    
    [ObservableProperty]
    private string _class   = string.Empty;

    [ObservableProperty]
    private string _career   = string.Empty;
    
    [ObservableProperty]
    private string _careerLevel = string.Empty;

    [ObservableProperty]
    private string _careerSchema = string.Empty;

    [ObservableProperty]
    private string _status = string.Empty;
 
    [ObservableProperty]
    private int _age;

    [ObservableProperty]
    private int _size;

    [ObservableProperty]
    private string _hairColor = string.Empty;

    [ObservableProperty]
    private string _eyesColor = string.Empty;
    
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

    public CharacterInformations GetModel()
    {
        return new CharacterInformations
        {
            Name = Name,
            Race = Race,
            Class = Class,
            Career = Career,
            CareerLevel = CareerLevel,
            CareerSchema = CareerSchema,
            Status = Status,
            Age = Age,
            Size = Size,
            HairColor = HairColor,
            EyesColor = EyesColor
        };
    }
}