using System.Collections.Generic;
using System.IO;
using System.Linq;
using GrandTextAdventure.Core.Entities;
using LibObjectFile.Elf;

namespace GrandTextAdventure.Core
{
    public class GameObjectReader
    {
        private readonly ElfBinarySection _codeSection;
        private readonly ulong _objectCount;
        private readonly BinaryReader _objectReader;
        private readonly Stream _strm;
        private readonly ElfSymbolTable _symTable;
        private ElfObjectFile _file;
        private ulong _objectIndex;

        public GameObjectReader(Stream strm)
        {
            _file = ElfObjectFile.Read(strm);
            _strm = strm;

            _symTable = (ElfSymbolTable)_file.Sections.FirstOrDefault(_ => _ is ElfSymbolTable);
            _codeSection = (ElfBinarySection)_file.Sections.FirstOrDefault(_ => _ is ElfBinarySection);

            _objectReader = new(_codeSection.Stream);

            _objectCount = _file.EntryPointAddress;
        }

        public ulong Count => _objectCount;
        public bool HasUnloadedObject => _objectIndex < _objectCount;
        public bool IsClosed { get; set; }

        public void Close()
        {
            _strm.Close();
            _file = null;
            IsClosed = true;
        }

        public GameObject ReadObject()
        {
            if (!IsClosed && _strm.Position != _strm.Length)
            {
                var type = (GameObjectType)_objectReader.ReadInt32();
                var instance = CreateObjectInstance(type);

                if (instance != null)
                {
                    instance.Name = _objectReader.ReadString();

                    var propCount = _objectReader.ReadInt32();
                    for (var i = 0; i < propCount; i++)
                    {
                        var symbolIndex = _objectReader.ReadUInt64();
                        var symbol = GetSymbolByIndex(symbolIndex);

                        var value = _objectReader.ReadInt32();

                        instance.Properties.Add(symbol, value);
                    }
                }
                _objectIndex++;

                return instance;
            }

            return null;
        }

        private static GameObject CreateObjectInstance(GameObjectType type)
        {
            return type switch
            {
                GameObjectType.Building => new Building(),
                GameObjectType.Charackter => new Charackter(),
                GameObjectType.Vehicle => new Vehicle(),
                GameObjectType.Weapon => new Weapon(),
                _ => null,
            };
        }

        private string GetSymbolByIndex(ulong value)
        {
            foreach (var sym in _symTable.Entries)
            {
                if (sym.Value == value)
                {
                    return sym.Name;
                }
            }

            throw new KeyNotFoundException("Symbol not found");
        }
    }
}
