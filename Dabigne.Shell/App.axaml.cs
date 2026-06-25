using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.Core.Services;
//using Application.Core.Utils;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Dabigne.Shell.Views;

namespace Dabigne.Shell;

public partial class App : Avalonia.Application
{
	private const string ModuleFolder = "Modules";
	private const string ModulePrefix = "Module.";
	private const string ModuleSuffix = ".dll";
    
	private InstanceProvider _instanceProvider = new();

	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			//AvaloniaUtils.DisableAvaloniaDataAnnotationValidation();
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
			new ApplicationModule()
		};

		var dynamicModules = Directory.GetFiles(ModuleFolder)
			.Where(f => f.StartsWith($"{ModuleFolder}{Path.DirectorySeparatorChar}{ModulePrefix}") 
			            && f.EndsWith(ModuleSuffix))
			.ToList();
		
		foreach (var module in dynamicModules)
		{
			var assembly = Assembly.LoadFrom(module);
			var types = assembly.GetTypes();
			var moduleTypes = types.Where(t => t.BaseType == typeof(Autofac.Module));
			foreach (var moduleType in moduleTypes)
			{
				modules.Add((Activator.CreateInstance(moduleType) as Autofac.Module)!);
			}
		}
        
		_instanceProvider.Start(modules);
	}
}