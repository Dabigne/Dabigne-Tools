using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;

namespace Module.PdfTools.Services;

public static class WordExtractorOptionsProvider
{
	public static NearestNeighbourWordExtractor.NearestNeighbourWordExtractorOptions GetOptions()
	{
		var wordExtractorOptions = new NearestNeighbourWordExtractor.NearestNeighbourWordExtractorOptions()
		{
			Filter = (pivot, canditate) =>
			{
				if (string.IsNullOrWhiteSpace(canditate.Value))
					return false;

				if (canditate.Value.Equals("\n"))
					return false;
				
				if (canditate.FontName != pivot.FontName)
					return false;

				return true;
			}
		};
		
		return wordExtractorOptions;
	}
}