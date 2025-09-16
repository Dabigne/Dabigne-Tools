using System.Collections.ObjectModel;
using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.PdfTools.ViewModels.Components;

namespace Module.PdfTools.ViewModels;

public class PdfMergeViewModelDesign : PdfMergeViewModel
{
    public PdfMergeViewModelDesign() : base(null, null, null){}
}

public partial class PdfMergeViewModel : ObservableObject
{
    private readonly IPdfService _pdfService;
    private readonly IFileService _fileService;

    public FileListViewModel FileList { get; }
    
    [ObservableProperty]
    private string _mergedPdfPath;
    
    public PdfMergeViewModel(
        FileListViewModel fileList, 
        IPdfService pdfService,
        IFileService fileService)
    {
        FileList = fileList;
        _pdfService = pdfService;
        _fileService = fileService;
    }
    
    [RelayCommand]
    private async Task PickSaveFile()
    {
        var file = await _fileService.PickSaveFile();
        if (file != null)
            MergedPdfPath = file.Path.AbsolutePath;
    }
    
    [RelayCommand]
    private void Merge()
    {
        _pdfService.MergePdfsIntoOne(FileList.Files.ToList(), MergedPdfPath);
    }
}