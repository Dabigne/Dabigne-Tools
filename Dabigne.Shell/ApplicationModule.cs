using Autofac;
using Dabigne.Shell.ViewModels;
using Dabigne.Shell.Views;

namespace Dabigne.Shell;

public class ApplicationModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<MainWindowViewModel>().AsSelf();
		builder.RegisterType<MainWindow>().AsSelf();
	}
}