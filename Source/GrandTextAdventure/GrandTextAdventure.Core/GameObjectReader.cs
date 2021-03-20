using System.IO;
using LibObjectFile.Elf;

namespace GrandTextAdventure.Core
{
    public class GameObjectReader
    {
        private readonly Stream _strm;
        private ElfObjectFile _file;

        public GameObjectReader(Stream strm)
        {
            _file = ElfObjectFile.Read(strm);
            _strm = strm;
        }

        public void Close()
        {
            _strm.Close();
        }
    }
}
