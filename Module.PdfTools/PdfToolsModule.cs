using Autofac;
using Module.PdfTools.Interfaces;
using Module.PdfTools.Services;
using Module.PdfTools.ViewModels;
using Module.PdfTools.ViewModels.Components;
using Module.PdfTools.Views;

namespace Module.PdfTools;

public class PdfToolsModule: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
	    builder.RegisterType<PdfToMarkdownService>().As<IPdfToMarkdownService>().InstancePerDependency();
	    builder.RegisterType<PdfTextBlockStringService>().As<IPdfTextBlockStringService>().InstancePerDependency();
	    
        builder.RegisterType<PdfMergeView>().AsSelf();
        builder.RegisterType<PdfToMarkdownView>().AsSelf();
        
        builder.RegisterType<FileListViewModel>().AsSelf();
        builder.RegisterType<PdfMergeViewModel>().AsSelf();
        builder.RegisterType<PdfToMarkdownViewModel>().AsSelf();
    }
}