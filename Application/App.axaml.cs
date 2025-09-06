using System.Collections.Generic;
using Application.Core;
using Application.Core.Interfaces.Services;
using Application.Core.Services;
using Application.Core.Utils;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Application.ViewModels;
using Application.Views;
using Autofac;
using Module.MangaDownload;

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
            new ApplicationModule(),
            new MangaDownloadModule()
        };
        _instanceProvider.Start(modules);
    }
}