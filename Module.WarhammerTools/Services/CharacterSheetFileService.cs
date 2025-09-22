using Module.WarhammerTools.Models;
using Newtonsoft.Json;

namespace Module.WarhammerTools.Services;

public class CharacterSheetFileService
{
    public CharacterSheet Load(string fileName)
    {
        var reader = new JsonTextReader(new StringReader(File.ReadAllText(fileName)));
        var serializer = new JsonSerializer();
        return serializer.Deserialize<CharacterSheet>(reader) ?? new CharacterSheet();
    }

    public void Save(CharacterSheet characterSheet, string fileName)
    {
        var writer = new JsonTextWriter(new StreamWriter(fileName));
        var serializer = new JsonSerializer();
        serializer.Serialize(writer, characterSheet, typeof(CharacterSheet));
        writer.Flush();
        writer.Close();
    }
}