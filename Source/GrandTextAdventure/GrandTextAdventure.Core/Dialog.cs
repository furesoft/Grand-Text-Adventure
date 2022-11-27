using System;
using System.IO;
using Darlek.Scheme;
using Spectre.Console;

namespace GrandTextAdventure.Core;

public static class Dialog
{
    public static DialogItem Load(string filename)
    {
        var s = new Script();
        s.Source = File.ReadAllText(filename);

        s.interpreter.DefineGlobal(Symbol.FromString("dialog-text"), new NativeProcedure(_ =>
        {
            var text = _[0].ToString();
            DialogItem next = null;

            if (_.Count == 2)
            {
                if (_[1] is DialogItem di)
                {
                    next = di;
                }
            }

            return new TextDialogItem(text) { Next = next };
        }));

        s.interpreter.DefineGlobal(Symbol.FromString("dialog-text"), new NativeProcedure(_ =>
        {
            var text = _[0].ToString();
            var callback = _[1] as ICallable;
            DialogItem next = null;

            if (_.Count == 3)
            {
                if (_[2] is DialogItem di)
                {
                    next = di;
                }
            }

            return new CallableActionDialogItem(text, callback, next);
        }));

        //ToDo: add choosedialogitem to interpreter

        return (DialogItem)s.Execute();
    }

    public static void Start(DialogItem root)
    {
        if (root is ChooseDialogItem cdi)
        {
            var current = cdi;

            foreach (var item in current.IntroductionLines)
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

                        Console.WriteLine(item);
                    }
                }
            }

            if (cdi.Children != null)
            {
                var selector = new SelectionPrompt<DialogItem>();
                selector.Converter = _ => _.Title;
                selector.AddChoices(cdi.Children);

                var selected = AnsiConsole.Prompt(selector);

                selected.Parent = root;
                Start(selected);
            }
        }
        else if (root is ActionDialogItem adi)
        {
            adi.Parent = root;
            adi.Invoke();
        }
        else if (root is TextDialogItem)
        {
            Console.WriteLine(root.Title);
        }

        if (root.Next != null)
        {
            Start(root.Next);
        }
    }
}
