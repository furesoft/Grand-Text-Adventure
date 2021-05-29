using System;

namespace GrandTextAdventure.Core
{
    public class Dialog
    {
        public static void Start(DialogItem root)
        {
            var current = root;
            while (current != null)
            {
                foreach (var item in current.Lines)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item == "<wait>")
                        {
                            GameEngine.Instance.Wait(2000);
                        }
                        else
                        {
                            GameEngine.Instance.Wait(2000);

                            if (!string.IsNullOrEmpty(current.Name))
                            {
                                Console.WriteLine(current.Name + ": " + item);
                            }
                            else
                            {
                                Console.WriteLine(item);
                            }
                        }
                    }
                }

                if (current.ChooseLines != null)
                {
                    // ToDo: Implement choosing option from dialog
                }
                else
                {
                    current = current.Next;
                }
            }
        }
    }
}