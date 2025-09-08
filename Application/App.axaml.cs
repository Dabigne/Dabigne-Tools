using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Application.Core;
using Application.Core.Interfaces.Services;
using Application.Core.Services;
using Application.Core.Utils;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Application.Views;

namespace Application;

public sealed class App : Avalonia.Application
{
    private IInstanceProvider  _instanceProvider;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            AvaloniaUtils.DisableAvaloniaDataAnnotationValidation();
            LoadModules();
            desktop.MainWindow = _instanceProvider.GetInstance<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void LoadModules()
    {
        _instanceProvider = new InstanceProvider();
        var modules = new List<Autofac.Module>
        {
            new CoreModule(),
            new ApplicationModule()
        };

        var dynamicModules = Directory.GetFiles("Modules")
            .Where(f => f.EndsWith(".dll"))
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