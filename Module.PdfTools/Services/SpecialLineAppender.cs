using System.Text;
using UglyToad.PdfPig.DocumentLayoutAnalysis;

namespace Module.PdfTools.Services;

public class SpecialLineAppender
{
	public string Format { get; set; }
	
	public Func<TextLine, bool> Filter { get; set; }
	
	public bool AppendLine(StringBuilder sb, TextLine line)
	{
		if (!Filter(line))
			return false;

		sb.AppendLine($"{Format} {line.Text}");
		
		return true;
	}
}