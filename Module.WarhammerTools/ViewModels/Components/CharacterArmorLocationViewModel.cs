using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterArmorLocationViewModel : ObservableObject
{
    public ArmorLocationId Id { get; set; }
    
    [ObservableProperty] 
    private string _hitScore;

    [ObservableProperty]
    private int _value;

    [ObservableProperty] 
    private string _title;
}