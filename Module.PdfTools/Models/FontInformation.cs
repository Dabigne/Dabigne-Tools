using Module.PdfTools.Interfaces;

namespace Module.PdfTools.Models;

public class FontInformation : IFontInformation
{
	public string Status { get; set; }
	
	public string FontName { get; set; }
	
	public int Weight { get; set; }
	
	public bool IsBold { get; set; }
	
	public bool IsItalic { get; set; }
	
	public IList<string> Words { get; } = [];
}