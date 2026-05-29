using System.Text;
using Module.PdfTools.Interfaces;
using Module.PdfTools.Models;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;
using UglyToad.PdfPig.DocumentLayoutAnalysis.ReadingOrderDetector;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using UglyToad.PdfPig.PdfFonts;

namespace Module.PdfTools.Services;

public class PdfToMarkdownService(
	IPdfTextBlockStringService pdfTextBlockStringService)
	: IPdfToMarkdownService
{
	private const string Footer = "Olivier Cueilliez - olivier.cueilliez@gmail.com";
	private const string H1FontName = "OEEHTP+CaslonAntique";
	
	private PdfDocument? _pdfDocument;
	
	public IPdfInformation OpenPdf(string path)
	{
		_pdfDocument?.Dispose();

		_pdfDocument = PdfDocument.Open(path);
		return new PdfInformation(_pdfDocument.NumberOfPages);
	}

	private string CheckPageValidity(int pageNumber)
	{
		if (_pdfDocument == null)
			return "No document opened";
		if (pageNumber < 1)
			return "Page number must be greater than or equal to 1.";
		if (pageNumber > _pdfDocument.NumberOfPages)
			return $"Page number must be less than or equal to {_pdfDocument.NumberOfPages}.";

		return string.Empty;
	}

	public string GetMarkDownFromPdfPage(int pageNumber)
	{
		var error = CheckPageValidity(pageNumber);
		if (error != string.Empty)
			return error;
		
		var page = _pdfDocument.GetPage(pageNumber);
		
		var letters = page.Letters;
		var wordExtractor = new NearestNeighbourWordExtractor(WordExtractorOptionsProvider.GetOptions());
		var words = wordExtractor.GetWords(letters);
		
		var textBlocks = DocstrumBoundingBoxes.Instance.GetBlocks(words);
		var orderedTextBlock = UnsupervisedReadingOrderDetector.Instance.Get(textBlocks);
		var filteredTextBlock = PdfTextBlockFilter.Apply(orderedTextBlock);
		
		return pdfTextBlockStringService.GetStringFromTextBlocks(filteredTextBlock);
	}

	public IList<IFontInformation> GetFontsInformationFromPdfPage(int pageNumber)
	{
		var error = CheckPageValidity(pageNumber);
		if (error != string.Empty)
			return [new FontInformation{Status = error}];
		
		var page = _pdfDocument.GetPage(pageNumber);
		var letters = page.Letters;
		var wordExtractor = new NearestNeighbourWordExtractor(WordExtractorOptionsProvider.GetOptions());
		var words = wordExtractor.GetWords(letters);

		var list = new List<IFontInformation>();
		foreach (var word in words.Where(w => !string.IsNullOrWhiteSpace(w.Text)))
		{
			var fontDetails = word.Letters[0].FontDetails;
			var information = GetExistingFontInformation(list, fontDetails);
			if (information == null)
				list.Add(GetNewFontInformation(fontDetails, word.Text));
			else
				information.Words.Add(word.Text);
		}
		
		return list;
	}

	private IFontInformation? GetExistingFontInformation(IList<IFontInformation> list, FontDetails fontDetails)
	{
		return list.FirstOrDefault(i =>
			i.FontName == fontDetails.Name 
			&& i.Weight == fontDetails.Weight);
	}
	
	private FontInformation GetNewFontInformation(FontDetails fontDetails, string text)
	{
		var information =  new FontInformation
		{
			FontName = fontDetails.Name, 
			Weight = fontDetails.Weight,
			IsBold = fontDetails.IsBold, 
			IsItalic = fontDetails.IsItalic
		};
		information.Words.Add(text);
		return information;
	}
}