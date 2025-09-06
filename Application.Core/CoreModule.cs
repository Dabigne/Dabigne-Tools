using Application.Core.Interfaces.Services;
using Application.Core.Services;
using Autofac;

namespace Application.Core;

public class CoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
        builder.RegisterType<ImageDownloaderService>().As<IImageDownloaderService>();
        builder.RegisterType<PdfService>().As<IPdfService>();
    }
}