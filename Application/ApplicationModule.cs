using Application.ViewModels;
using Application.Views;
using Autofac;

namespace Application;

public class ApplicationModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MainWindowViewModel>().AsSelf();
        builder.RegisterType<MainWindow>().AsSelf();
    }
}