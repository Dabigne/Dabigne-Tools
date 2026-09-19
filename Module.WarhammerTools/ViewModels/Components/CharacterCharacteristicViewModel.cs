using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterCharacteristicViewModel(ICharacterSheetService characterSheetService)
	: ObservableObject, IViewModel<CharacterCharacteristic>
{
	private CharacterCharacteristic? _characteristic;
	
    [ObservableProperty]
    private string _name = string.Empty;
    
    [ObservableProperty]
    private string _shortcut = string.Empty;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentValue))]
    private int _initialValue;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentValue))]
    private int _improvements;

    public int CurrentValue => InitialValue + Improvements;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
	    base.OnPropertyChanged(e);
	    
	    if(_characteristic != null)
	    {
		    _characteristic.Name = Name;
		    _characteristic.ShortCut = Shortcut;
		    _characteristic.InitialValue = InitialValue;
		    _characteristic.Improvments = Improvements;
	    }
	    
	    characterSheetService.UpdateCharacterSheet();
    }

    public void SetModel(CharacterCharacteristic model)
    {
        _characteristic = model;

#pragma warning disable MVVMTK0034
        _name = _characteristic.Name;
        _shortcut = _characteristic.ShortCut;
        _initialValue = _characteristic.InitialValue;
        _improvements = _characteristic.Improvments;
#pragma warning restore MVVMTK0034
    }

    public CharacterCharacteristic GetModel()
    {
        return _characteristic ?? new CharacterCharacteristic(Name, Shortcut, InitialValue, Improvements);
    }
}