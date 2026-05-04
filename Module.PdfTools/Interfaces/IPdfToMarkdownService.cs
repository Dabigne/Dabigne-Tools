using PdfSharp.Pdf;

namespace Module.PdfTools.Interfaces;

public interface IPdfInformation
{
	int PageNumber { get; }
}

public interface IPdfToMarkdownService
{
	IPdfInformation OpenPdf(string path);
	
	string GetMarkDownFromPdfPage(int pageNumber);
}