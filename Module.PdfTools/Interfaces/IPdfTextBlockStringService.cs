using UglyToad.PdfPig.DocumentLayoutAnalysis;

namespace Module.PdfTools.Interfaces;

public interface IPdfTextBlockStringService
{
	string GetStringFromTextBlocks(IEnumerable<TextBlock> textBlocks);
}