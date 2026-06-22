using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.PdfTools.Interfaces;
using Module.PdfTools.ViewModels.Components;

namespace Module.PdfTools.ViewModels;

public class PdfToMarkdownViewModelDesign : PdfToMarkdownViewModel
{
	public PdfToMarkdownViewModelDesign() : base(null, null){}
}

public partial class PdfToMarkdownViewModel(
	IFileService  fileService,
	IPdfToMarkdownService  pdfToMarkdownService) 
	: ObservableObject
{
	private IPdfInformation? _pdfInformation;
	
	[ObservableProperty] 
	private string? _pdfPath;
	
	[ObservableProperty]
	private string? _pageText;
	
	[ObservableProperty]
	private int _pageNumber;
	
	[ObservableProperty]
	private FontsInformationViewModel  _fontsInformation = new();
	
	[RelayCommand]
	private async Task PickFile()
	{
		var file = await fileService.PickLoadFile();
		if (file == null)
			return;
		
		PdfPath = file.Path.LocalPath;
		_pdfInformation =  pdfToMarkdownService.OpenPdf(PdfPath);
		PageNumber = 1;
	}

	[RelayCommand]
	private async Task ExportPdf()
	{
		if (_pdfInformation == null)
			return;
		
		var folder = await fileService.PickFolder();
		if (folder == null)
			return;

		pdfToMarkdownService.ExportPdfToFolder(folder);
	}
	
	[RelayCommand]
	private async Task ExportPdfImages()
	{
		if (_pdfInformation == null)
			return;
		
		var folder = await fileService.PickFolder();
		if (folder == null)
			return;

		pdfToMarkdownService.ExportImagesToFolder(folder);
	}
	
	partial void OnPageNumberChanged(int value)
	{
		if (_pdfInformation == null)
			return;
		if (value < 1)
			return;
		if (value > _pdfInformation.PageCount)
			return;
		
		FontsInformation.Init(pdfToMarkdownService.GetFontsInformationFromPdfPage(value));
		PageText = pdfToMarkdownService.GetMarkDownFromPdfPage(value);
	}
}