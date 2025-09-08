using Autofac;
using Module.MangaDownload.Interfaces;
using Module.MangaDownload.Services;
using Module.MangaDownload.ViewModels;
using Module.MangaDownload.Views;

namespace Module.MangaDownload;

public class MangaDownloadModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MangaDownloadView>().AsSelf();
        
        builder.RegisterType<MangaDownloadViewModel>().AsSelf();

        builder.RegisterType<CatalogService>().As<ICatalogService>().SingleInstance();
        builder.RegisterType<MangaPdfService>().As<IMangaPdfService>().SingleInstance();
    }
}