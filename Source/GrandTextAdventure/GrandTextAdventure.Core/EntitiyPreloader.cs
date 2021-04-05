using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandTextAdventure.Core
{
    public static class EntitiyPreloader
    {
        public static void PreLoad(string path)
        {
            var files = Directory.GetFiles(path, "*.ced");
            foreach (var file in files)
            {
                var reader = new GameObjectReader(File.OpenRead(file));

                while (reader.HasUnloadedObject)
                {
                    var obj = reader.ReadObject();

                    GameObjectTable.Add(obj);
                }

                reader.Close();
            }
        }
    }
}
