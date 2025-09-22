using CommunityToolkit.Mvvm.ComponentModel;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterInformationsViewModel : ObservableObject
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
}