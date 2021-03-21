using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GrandTextAdventure.Core.Entities;
using LibObjectFile.Elf;

namespace GrandTextAdventure.Core
{
    public class GameObjectReader
    {
        private readonly ulong _objectCount;
        private ElfBinarySection _codeSection;
        private ElfObjectFile _file;
        private ulong _objectIndex;
        private BinaryReader _objectReader;
        private Stream _strm;
        private ElfStringTable _strTable;
        private ElfSymbolTable _symTable;

        public GameObjectReader(Stream strm)
        {
            _file = ElfObjectFile.Read(strm);
            _strm = strm;

            _symTable = (ElfSymbolTable)_file.Sections.FirstOrDefault(_ => _ is ElfSymbolTable);
            _strTable = (ElfStringTable)_file.Sections.FirstOrDefault(_ => _ is ElfStringTable);
            _codeSection = (ElfBinarySection)_file.Sections.FirstOrDefault(_ => _ is ElfBinarySection);

            _objectReader = new(_codeSection.Stream);

            _objectCount = _file.EntryPointAddress;
        }

        public ulong Count => _objectCount;
        public bool HasUnloadedObject => _objectIndex < _objectCount;
        public bool IsClosed { get; set; }

        public void Close()
        {
            _objectReader.Close();
            _file = null;
            IsClosed = true;
            _symTable = null;
            _strTable = null;
            _strm = null;
            _codeSection = null;
            _objectReader = null;
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

                        var propTypeCode = (TypeCode)_objectReader.ReadByte();
                        var value = GetValue(propTypeCode);

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

        private object GetValue(TypeCode propTypeCode)
        {
            switch (propTypeCode)
            {
                case TypeCode.Boolean:
                    return _objectReader.ReadBoolean();

                case TypeCode.Int32:
                    return _objectReader.ReadInt32();

                case TypeCode.Double:
                    return _objectReader.ReadDouble();

                case TypeCode.String:
                    var index = _objectReader.ReadUInt32();
                    _strTable.TryFind(index, out var strVal);

                    return strVal;

                default:
                    return _objectReader.ReadInt32();
            }
        }
    }
}
