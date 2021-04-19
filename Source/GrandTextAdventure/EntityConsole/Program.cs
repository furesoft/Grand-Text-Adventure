using System;
using System.IO;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Entities;

namespace EntityConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var input = ReadLine.Read("> ");

                ReadLine.AddHistory(input);

                var cmd = Command.FromString(input);
                switch (cmd.Name)
                {
                    case "list":
                        foreach (var obj in GameObjectTable.GetAll())
                        {
                            System.Console.WriteLine(obj.Name);
                        }
                        break;
                    case "load":
                        var gor = new GameObjectReader(File.OpenRead(cmd.Args[0]));

                        while (gor.HasUnloadedObject)
                        {
                            GameObjectTable.Add(gor.ReadObject());
                        }

                        gor.Close();
                        break;
                    case "save":
                        var gow = new GameObjectWriter(File.OpenWrite(cmd.Args[0]));

                        foreach (var obj in GameObjectTable.GetAll())
                        {
                            gow.WriteObject(obj);
                        }

                        gow.Close();

                        break;

                    case "add-vehicle":
                        GameObjectTable.Add(new Vehicle() { Name = cmd.Args[0] });
                        break;
                    case "add-npc":
                        GameObjectTable.Add(new NPC() { Name = cmd.Args[0] });
                        break; ;
                    case "add-weapon":
                        GameObjectTable.Add(new Weapon() { Name = cmd.Args[0] });
                        break; ;
                    case "remove-object":
                        GameObjectTable.Remove(cmd.Args[0]);

                        break;
                    case "set-property":
                        var oName1 = cmd.Args[0];
                        var pName1 = cmd.Args[1];
                        var pValue = cmd.Args[2];

                        GameObjectTable.GetEntity(oName1).SetOrAddValue(pName1, pValue);
                        break;
                    case "remove-property":
                        var oName = cmd.Args[0];
                        var pName = cmd.Args[1];

                        GameObjectTable.GetEntity(oName).Properties.Remove(pName);
                        break;
                }
            }
        }
    }
}
