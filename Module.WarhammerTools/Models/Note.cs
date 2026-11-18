namespace Module.WarhammerTools.Models;

public class Note
{
    public string Title { get; set; } = string.Empty;
    
    public string Text { get; set; } = string.Empty;
    
    public List<NoteBlock> Blocks { get; set; } = new List<NoteBlock>();
}

public class NoteBlock
{
	public string Text { get; set; } = string.Empty;
}