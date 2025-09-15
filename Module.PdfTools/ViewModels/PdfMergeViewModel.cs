using System.Collections.ObjectModel;
using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Module.PdfTools.ViewModels;

public class PdfMergeViewModelDesign : PdfMergeViewModel
{
    public PdfMergeViewModelDesign() : base(null, null){}
}

public partial class PdfMergeViewModel : ObservableObject
{
    private readonly IPdfService _pdfService;
    private readonly IFileService _fileService;

    [ObservableProperty]
    private string _mergedPdfPath;
    
    public ObservableCollection<string> PdfPaths { get; } = [];
    
    public PdfMergeViewModel(IPdfService pdfService, IFileService fileService)
    {
        _pdfService = pdfService;
        _fileService = fileService;
    }

    [RelayCommand]
    private async Task PickFiles()
    {
        var files = await _fileService.PickFiles();
        foreach (var storageFile in files)
        {
            PdfPaths.Add(storageFile.Path.AbsolutePath);
        }
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
        _pdfService.MergePdfsIntoOne(PdfPaths.ToList(), MergedPdfPath);
    }
}