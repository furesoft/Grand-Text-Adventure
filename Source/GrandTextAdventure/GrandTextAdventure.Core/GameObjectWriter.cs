using System;
using System.Collections.Generic;
using System.IO;
using LibObjectFile.Elf;

namespace GrandTextAdventure.Core
{
    public class GameObjectWriter
    {
        private readonly ElfBinarySection _codeSection;
        private readonly MemoryStream _codeStream = new();
        private readonly ElfObjectFile _file = new(ElfArch.ARM);
        private readonly Stream _outputStream;
        private readonly ElfSymbolTable _symbolTable = new();
        private ulong _lastPropertyIndex = 0;

        public GameObjectWriter(Stream outputStream)
        {
            _outputStream = outputStream;

            _file.FileClass = ElfFileClass.Is32;

            _codeSection = new ElfBinarySection(_codeStream).ConfigureAs(ElfSectionSpecialType.Text);

            var stringSection = new ElfStringTable();
            _file.AddSection(stringSection);

            _symbolTable.Link = stringSection;
        }

        public void Close()
        {
            _file.AddSection(_symbolTable);
            _file.AddSection(_codeSection);

            _file.AddSection(new ElfSectionHeaderStringTable());
            _file.Write(_outputStream);
        }

        public void WriteObject(GameObject obj)
        {
            // append properties to index section
            // write object to Objects Section with index instead of propertyname

            foreach (var prop in obj.Properties)
            {
                AddSymbol(prop.Key);
            }

            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);

            bw.Write((int)obj.Type);
            bw.Write(obj.Name);

            foreach (var prop in obj.Properties)
            {
                bw.Write(GetIndexOfSymbol(prop.Key));
                bw.Write((int)prop.Value);
            }

            ms.CopyTo(_codeStream);
            bw.Close();
        }

        private void AddSymbol(string name)
        {
            foreach (var symbol in _symbolTable.Entries)
            {
                if (symbol.Name == name)
                {
                    return;
                }
            }

            _symbolTable.Entries.Add(
                new ElfSymbol()
                {
                    Bind = ElfSymbolBind.Global,
                    Name = name,
                    Value = _lastPropertyIndex++,
                    Type = ElfSymbolType.Object,
                    Section = _codeSection,
                    Size = sizeof(int)
                });
        }

        private ulong GetIndexOfSymbol(string name)
        {
            foreach (var symbol in _symbolTable.Entries)
            {
                if (symbol.Name == name)
                {
                    return symbol.Value;
                }
            }

            throw new KeyNotFoundException();
        }
    }
}
