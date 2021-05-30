using System;
using System.Collections.Generic;
using LiteDB;

namespace GrandTextAdventure.Core
{
    public static class Phone
    {
        private static readonly Dictionary<string, Action> s_numberHandlers = new();
        private static Dictionary<string, string> s_contacts = new();

        public static void Dial(string number)
        {
            if (s_contacts.ContainsKey(number))
            {
                number = s_contacts[number]; // use number from contact list
            }

            if (s_numberHandlers.ContainsKey(number))
            {
                s_numberHandlers[number]();
            }
            else
            {
                Console.WriteLine("Number is not reachable or not registered");
            }
        }

        public static void AddNumberHandler(string number, Action handler)
        {
            s_numberHandlers.Add(number, handler);
        }

        public static void AddContact(string name, string number)
        {
            if (!s_contacts.ContainsKey(name))
            {
                s_contacts.Add(name, number);
            }
        }

        public static IEnumerable<string> GetContactNames()
        {
            return s_contacts.Keys;
        }

        public static void Load()
        {
            using LiteDatabase db = new("gta.conf"); // ToDo: need to set config path
            var contactsCollection = db.GetCollection<KeyValuePair<string, string>>("contacts");

            s_contacts = new Dictionary<string, string>(contactsCollection.FindAll());
        }

        public static void Save()
        {
            using LiteDatabase db = new("gta.conf"); // ToDo: need to set config path
            var contactsCollection = db.GetCollection<KeyValuePair<string, string>>("contacts");

            foreach (var contact in s_contacts)
            {
                if (!contactsCollection.Exists(Query.EQ("Key", contact.Key)))
                {
                    contactsCollection.Insert(contact);
                }
            }
        }
    }
}