using System.Collections.Specialized;
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
    private int _pageCount;
    
    [ObservableProperty]
    private string _mergedPdfPath = string.Empty;
    
    public PdfMergeViewModel(
        FileListViewModel? fileList, 
        IPdfService? pdfService,
        IFileService? fileService)
    {
        FileList = fileList!;
        FileList.Files.CollectionChanged += FilesOnCollectionChanged;
        _pdfService = pdfService!;
        _fileService = fileService!;
    }

    private void FilesOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        PageCount = _pdfService.GetPageNumberFromFiles(FileList.Files);
    }

    [RelayCommand]
    private async Task PickSaveFile()
    {
        var file = await _fileService.PickSaveFile();
        if (file != null)
            MergedPdfPath = file.Path.LocalPath;
    }
    
    [RelayCommand]
    private void Merge()
    {
        _pdfService.MergePdfsIntoOne(FileList.Files.ToList(), MergedPdfPath);
    }
}