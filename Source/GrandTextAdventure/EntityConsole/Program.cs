using System;

namespace EntityConsole
{
    class Program
    {
        // load-file
        // create-file
        // save file

        // add-object (vehicle|npc|weapon)
        // delete-object
        // add-property
        // delete-property
        // change-property

        static void Main(string[] args)
        {
            while (true)
            {
                var input = ReadLine.Read("> ");

                ReadLine.AddHistory(input);

                var cmd = Command.FromString(input);

            }
        }
    }
}
