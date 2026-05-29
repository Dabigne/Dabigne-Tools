using UglyToad.PdfPig.DocumentLayoutAnalysis;

namespace Module.PdfTools.Services;

public static class PdfTextBlockFilter
{
	private const string Footer = "Olivier Cueilliez - olivier.cueilliez@gmail.com";

	public static IList<TextBlock> Apply(IEnumerable<TextBlock> textBlocks)
	{
		return textBlocks
			.Where(t => t.Text != Footer && t.Text.Length > 1)
			.ToList();
	}
}