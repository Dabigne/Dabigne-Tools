using Autofac;
using Dabigne.Tools.Views;
using Dabigne.Tools.ViewModels;

namespace Dabigne.Tools;

public class ApplicationModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MainWindowViewModel>().AsSelf();
        builder.RegisterType<MainWindow>().AsSelf();
    }
}