using Module.PdfTools.Interfaces;

namespace Module.PdfTools.Models;

public class PdfInformation(int pageNumber) 
	: IPdfInformation
{
	public int PageCount { get; } = pageNumber;
}