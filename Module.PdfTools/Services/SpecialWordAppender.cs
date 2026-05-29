using System.Text;
using UglyToad.PdfPig.Content;

namespace Module.PdfTools.Services;

public class SpecialWordAppender
{
	public bool Started { get; set; }
	
	public string Format { get; set; }
	
	public Func<Word, bool> Filter { get; set; }

	public bool AppendWord(StringBuilder sb, Word word, bool isFirstWord)
	{
		if (Filter(word) && !Started)
		{
			Started = true;
			if (isFirstWord)
				sb.Append($"{Format}{word}");
			else
				sb.Append($" {Format}{word}");
			return true;
		}
		
		if (!Filter(word) && Started)
		{
			Started = false;
			sb.Append($"{Format} {word}");
			return true;
		}
		return false;
	}

	public void FinalizeLine(StringBuilder sb)
	{
		if (!Started)
			return;
		sb.Append(Format);
	}
}