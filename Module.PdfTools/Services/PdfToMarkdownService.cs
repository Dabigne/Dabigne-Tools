using System.Text;
using Module.PdfTools.Interfaces;
using Module.PdfTools.Models;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;
using UglyToad.PdfPig.DocumentLayoutAnalysis.ReadingOrderDetector;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;

namespace Module.PdfTools.Services;

public class PdfToMarkdownService : IPdfToMarkdownService
{
	private PdfDocument? _pdfDocument;
	
	public IPdfInformation OpenPdf(string path)
	{
		_pdfDocument?.Dispose();

		_pdfDocument = PdfDocument.Open(path);
		return new PdfInformation(_pdfDocument.NumberOfPages);
	}

	public string GetMarkDownFromPdfPage(int pageNumber)
	{
		if (_pdfDocument == null)
			return "No document opened";
		if (pageNumber < 1)
			return "Page number must be greater than or equal to 1.";
		if (pageNumber > _pdfDocument.NumberOfPages)
			return $"Page number must be less than or equal to {_pdfDocument.NumberOfPages}.";
		
		var page = _pdfDocument.GetPage(pageNumber);
		
		var letters = page.Letters;
		var words = NearestNeighbourWordExtractor.Instance.GetWords(letters);
		var textBlocks = DocstrumBoundingBoxes.Instance.GetBlocks(words);
		var orderedTextBlock = UnsupervisedReadingOrderDetector.Instance.Get(textBlocks);

		var sb = new StringBuilder();
		foreach (var block in orderedTextBlock)
		{
			sb.AppendLine(block.ReadingOrder.ToString());
			foreach (var blockTextLine in block.TextLines)
			{
				sb.AppendLine(blockTextLine.Text);
			}
		}

		return sb.ToString();
	}
}