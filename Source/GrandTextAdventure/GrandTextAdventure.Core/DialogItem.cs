using System.IO;

namespace GrandTextAdventure.Core
{
    public record DialogItem(string Name, string[] Lines, DialogItem Next, string[] ChooseLines = null)
    {
        public static DialogItem FromJsonStream(Stream strm)
        {
            var reader = new StreamReader(strm);
            var result = System.Text.Json.JsonSerializer.Deserialize<DialogItem>(reader.ReadToEnd());

            reader.Close();

            return result;
        }
    }
}