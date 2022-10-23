using System.IO;
using System.Text.Json;

namespace GrandTextAdventure.Core;

public record DialogItem(string Name, string[] Lines, DialogItem Next, string[] ChooseLines = null)
{
    public static DialogItem FromJsonStream(Stream strm)
    {
        var reader = new StreamReader(strm);
        var result = JsonSerializer.Deserialize<DialogItem>(reader.ReadToEnd());

        reader.Close();

        return result;
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
}