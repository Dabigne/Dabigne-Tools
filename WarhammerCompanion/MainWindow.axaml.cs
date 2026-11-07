using Application.Core.Interfaces.Services;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Module.WarhammerTools.Views;

namespace WarhammerCompanion;

public partial class MainWindow : Window
{
	private MainWindowViewModel ViewModel => (DataContext as MainWindowViewModel)!;
	
	public MainWindow(
		IFileService fileService,
		MainWindowViewModel viewModel)
	{
		fileService.SetWindow(this);
		
		DataContext = viewModel;
		
		InitializeComponent();
		
		Loaded += OnLoaded;
		Closing += (sender, args) => ViewModel.Close();
	}

	private void OnLoaded(object? sender, RoutedEventArgs e)
	{
		Content = new CharacterSheetView(ViewModel.CharacterSheet);
	}
}