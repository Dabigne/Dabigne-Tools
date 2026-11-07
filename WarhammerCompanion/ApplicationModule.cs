using Autofac;

namespace WarhammerCompanion;

public class ApplicationModule : Autofac.Module
{
	protected override void Load(Autofac.ContainerBuilder builder)
	{
		builder.RegisterType<MainWindowViewModel>().AsSelf();
		builder.RegisterType<MainWindow>().AsSelf();
	}
}