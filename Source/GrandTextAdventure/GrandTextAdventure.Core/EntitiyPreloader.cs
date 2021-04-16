using System.IO;

namespace GrandTextAdventure.Core
{
    public static class EntitiyPreloader
    {
        public static void PreLoad(string path)
        {
            var files = Directory.GetFiles(path, "*.ced");
            foreach (var file in files)
            {
                var fileStrm = File.OpenRead(file);
                PreLoad(fileStrm);
            }
        }

        public static void PreLoad(Stream strm)
        {
            var reader = new GameObjectReader(strm);

            while (reader.HasUnloadedObject)
            {
                var obj = reader.ReadObject();

                GameObjectTable.Add(obj);
            }

            reader.Close();
        }
    }
}
