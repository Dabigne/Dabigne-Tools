using Avalonia.Platform.Storage;
using PdfSharp.Pdf;

namespace Module.PdfTools.Interfaces;

public interface IPdfInformation
{
	int PageCount { get; }
}

public interface IFontInformation
{
	string Status { get; }
	
	string FontName { get; }
	
	int Weight { get; }

	public bool IsBold { get; set; }
	
	public bool IsItalic { get; set; }

	IList<string> Words { get; }
}

public interface IPdfToMarkdownService
{
	IPdfInformation OpenPdf(string path);
	
	string GetMarkDownFromPdfPage(int pageNumber);

	IList<IFontInformation> GetFontsInformationFromPdfPage(int pageNumber);
	
	void ExportPdfToFolder(IStorageFolder folder);

	void ExportImagesToFolder(IStorageFolder folder);
}