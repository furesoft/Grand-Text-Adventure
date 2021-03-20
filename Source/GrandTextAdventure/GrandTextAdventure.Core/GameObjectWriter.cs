using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibObjectFile;
using LibObjectFile.Elf;

namespace GrandTextAdventure.Core
{
    public class GameObjectWriter
    {
        private readonly MemoryStream _codeStream = new();
        private readonly ElfObjectFile _file = new(ElfArch.ARM);
        private readonly Stream _outputStream;
        private readonly ElfSymbolTable _symbolTable = new();
        private ElfBinarySection _codeSection;
        private ulong _lastPropertyIndex = 0;

        public GameObjectWriter(Stream outputStream)
        {
            _outputStream = outputStream;

            _file.FileClass = ElfFileClass.Is32;

            _file.AddSection(new ElfStringTable());
        }

        public bool IsClosed { get; set; }

        public void Close()
        {
            _codeSection = new ElfBinarySection(_codeStream).ConfigureAs(ElfSectionSpecialType.Text);

            _symbolTable.Link = _file.Sections.FirstOrDefault(_ => _ is ElfStringTable);

            _file.AddSection(_symbolTable);
            _file.AddSection(_codeSection);

            _file.AddSection(new ElfSectionHeaderStringTable());

            _file.Write(_outputStream);
            _outputStream.Flush();

            _outputStream.Close();
            _codeStream.Close();

            IsClosed = true;
        }

        public void WriteObject(GameObject obj)
        {
            // append properties to index section
            // write object to Objects Section with index instead of propertyname

            if (!IsClosed)
            {
                foreach (var prop in obj.Properties)
                {
                    AddSymbol(prop.Key);
                }

                var bw = new BinaryWriter(_codeStream);

                bw.Write((int)obj.Type);
                bw.Write(obj.Name);

                foreach (var prop in obj.Properties)
                {
                    bw.Write(GetIndexOfSymbol(prop.Key));
                    bw.Write((int)prop.Value);
                }
            }
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
                    //Size = sizeof(int)
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
