using Autofac;
using Module.PdfTools.ViewModels;
using Module.PdfTools.ViewModels.Components;
using Module.PdfTools.Views;

namespace Module.PdfTools;

public class PdfToolsModule: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PdfMergeView>().AsSelf();
        builder.RegisterType<PdfFromImagesView>().AsSelf();
        
        builder.RegisterType<PdfMergeViewModel>().AsSelf();
        builder.RegisterType<FileListViewModel>().AsSelf();
    }
}