using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.WarhammerTools.Models.Dice;

namespace Module.WarhammerTools.ViewModels;

public partial class DiceRollViewModel : ObservableObject
{
	[ObservableProperty]
	private DiceRollRequest? _currentRollRequest;

	[ObservableProperty]
	private string _rollResultText = "Select dice to roll";

	[ObservableProperty]
	private int _lastRollTotal;

	public ObservableCollection<string> RollHistory { get; } = new();

	[RelayCommand]
	private void RollSingle(DieType dieType)
	{
		// Always instantiate a new DiceRollRequest so AvaloniaProperty.Changed fires even on repeat rolls
		CurrentRollRequest = new DiceRollRequest(new[] { dieType });
	}

	[RelayCommand]
	private void RollD100()
	{
		CurrentRollRequest = new DiceRollRequest(System.Array.Empty<DieType>(), IsD100: true);
	}

	[RelayCommand]
	private void RollAttackCombo()
	{
		CurrentRollRequest = new DiceRollRequest(new[] { DieType.D20, DieType.D6, DieType.D6 });
	}

	[RelayCommand]
	private void OnRollCompleted(int total)
	{
		RollHistory.Insert(0, RollResultText);
	}
}
