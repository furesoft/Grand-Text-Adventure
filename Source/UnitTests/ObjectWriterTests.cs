using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace UnitTests;

[TestClass]
public class ObjectWriterTests
{
    [TestMethod]
    public void MyTestMethod()
    {
        var ow = new GameObjectWriter(File.OpenWrite("many.ced"));
        for (int i = 1; i < 10000000; i++)
        {
            dynamic n = new Vehicle();
            n.Name = "Vehicle " + i;

            n.hello = "world";

            ow.WriteObject(n);
        }
        for (int i = 1; i < 10000000; i++)
        {
            dynamic n = new Weapon();
            n.Name = "Weapon " + i;

            n.hello = "world";

            ow.WriteObject(n);
        }
        for (int i = 1; i < 10000000; i++)
        {
            dynamic n = new Vehicle();
            n.Name = "Vehicle " + i;

            n.hello = "world";

            ow.WriteObject(n);
        }
        for (int i = 1; i < 10000000; i++)
        {
            dynamic n = new Weapon();
            n.Name = "Weapon " + i;

            n.hello = "world";

            ow.WriteObject(n);
        }

        ow.Close();
    }

    [TestMethod]
    public void Write_Should_Pass()
    {
        dynamic lambo = new Vehicle
        {
            Name = "Lamborghini"
        };

        lambo.speed = 42;
        lambo.protection = 100;

        dynamic mg11 = new Weapon
        {
            Name = "mg11"
        };

        mg11.wrap = 3.14;
        mg11.speed = 12;
        mg11.protection = 0;

        mg11.blub = "hello world";

        var strm = File.Open("test.ced", FileMode.OpenOrCreate);
        var writer = new GameObjectWriter(strm);

        writer.WriteObject(mg11);
        writer.WriteObject(lambo);

        writer.Close();
    }
}