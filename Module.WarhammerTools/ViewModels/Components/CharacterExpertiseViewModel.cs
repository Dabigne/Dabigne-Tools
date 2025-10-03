using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterExpertiseViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _name;
    
    [ObservableProperty]
    private CharacterCharacteristic _characteristic;
    
    [ObservableProperty]
    private int _improvment;

    [ObservableProperty] 
    private int _totalValue;
}