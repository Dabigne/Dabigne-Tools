using System.Text;
using Module.PdfTools.Interfaces;
using UglyToad.PdfPig.DocumentLayoutAnalysis;

namespace Module.PdfTools.Services;

public class PdfTextBlockStringService : IPdfTextBlockStringService
{
	private readonly SpecialLineAppender _headerAppender = new()
	{
		Format = "#",
		Filter = line => line.Text == line.Text.ToUpper()
	};
	
	private readonly SpecialLineAppender _subHeaderAppender = new()
	{
		Format = "##",
		Filter = line => 
			line.Words[0].Letters[0].FontDetails is { Weight: > 600, IsBold: true } &&
			line.Words[^1].Letters[^1].FontDetails is { Weight: > 600, IsBold: true }
	};
	
	private readonly SpecialWordAppender _boldAppender = new()
	{
		Format = "**",
		Filter = word => word.Letters[0].FontDetails.IsBold
	};

	private readonly SpecialWordAppender _italicAppender = new()
	{
		Format = "*",
		Filter = word => word.Letters[0].FontDetails.IsItalic
	};
	
	public string GetStringFromTextBlocks(IEnumerable<TextBlock> textBlocks)
	{
		var sb = new StringBuilder();
		foreach (var block in textBlocks)
		{
			foreach (var blockTextLine in block.TextLines)
			{
				if (_subHeaderAppender.AppendLine(sb, blockTextLine))
					continue;
				if (_headerAppender.AppendLine(sb, blockTextLine))
					continue;
				
				AppendLine(sb, blockTextLine);
			}
		}

		return sb.ToString();
	}

	private void AppendLine(StringBuilder sb, TextLine line)
	{
		if (line.Words.Count == 0) 
			return;

		_boldAppender.Started = false;
		_italicAppender.Started = false;
		foreach (var word in line.Words)
		{
			var firstWord = word == line.Words[0];
			if (_boldAppender.AppendWord(sb, word, firstWord))
				continue;
			if (_italicAppender.AppendWord(sb, word, firstWord))
				continue;
			
			if (word != line.Words[0])
				sb.Append(' ');
			sb.Append($"{word.Text}");
		}
		
		_boldAppender.FinalizeLine(sb);
		_italicAppender.FinalizeLine(sb);
		
		sb.AppendLine();
	}
}