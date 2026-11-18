using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.WarhammerTools.Models.Dice;

namespace Module.WarhammerTools.ViewModels;

public partial class DicePoolItemViewModel : ObservableObject
{
	public DieType Type { get; }
	public string Label { get; }

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(HasDice))]
	private int _count;

	public bool HasDice => Count > 0;

	public DicePoolItemViewModel(DieType type, string label)
	{
		Type = type;
		Label = label;
	}

	[RelayCommand]
	private void Increment()
	{
		if (Count < 20) Count++;
	}

	[RelayCommand]
	private void Decrement()
	{
		if (Count > 0) Count--;
	}
}