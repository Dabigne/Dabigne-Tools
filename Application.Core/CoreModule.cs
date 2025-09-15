using Application.Core.Interfaces.Services;
using Application.Core.Services;
using Autofac;

namespace Application.Core;

public class CoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<FileService>().As<IFileService>().SingleInstance();
        builder.RegisterType<ImageDownloaderService>().As<IImageDownloaderService>().SingleInstance();
        builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
        builder.RegisterType<OutputService>().As<IOutputService>().SingleInstance();
        builder.RegisterType<PdfService>().As<IPdfService>();
    }
}