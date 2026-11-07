using System.Collections.Generic;
using Application.Core;
using Application.Core.Services;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Module.WarhammerTools;

namespace WarhammerCompanion;

public partial class App : Avalonia.Application
{
	private InstanceProvider _instanceProvider = new();

	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			LoadModules();
			desktop.MainWindow = _instanceProvider.GetInstance<MainWindow>();
		}

		base.OnFrameworkInitializationCompleted();
	}
	
	private void LoadModules()
	{
		var modules = new List<Autofac.Module>
		{
			new CoreModule(),
			new WarhammerToolsModule(),
			new ApplicationModule()
		};

		_instanceProvider.Start(modules);
	}
}