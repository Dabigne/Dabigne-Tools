using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.PdfTools.Interfaces;

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
	
	partial void OnPageNumberChanged(int value)
	{
		if (_pdfInformation == null)
			return;
		if (value < 1)
			return;
		if (value > _pdfInformation.PageNumber)
			return;
		
		PageText = pdfToMarkdownService.GetMarkDownFromPdfPage(value);
	}
}