﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GrandTextAdventure.Core
{
    public class GameObject
    {
        public delegate void GameObjectEventHandler(string property, object value);

        public int ID
        {
            get { return GetValue<int>("ID"); }
            set { SetOrAddValue("ID", value); }
        }

        public virtual void Init() { }

        public virtual void Deinit()
        {
            OnPropertyChanged = null;
            Properties = null;
        }

        public void ObserverProperty<T>(string name, Action<T> handler)
        {
            OnPropertyChanged += (prop, val) =>
            {
                if (prop.Equals(name))
                {
                    handler?.Invoke((T)val);
                }
            };
        }

        public event GameObjectEventHandler OnPropertyChanged;

        public string Name { get => GetValue<string>(nameof(Name)); set => SetOrAddValue(nameof(Name), value); }

        public Dictionary<string, object> Properties { get; set; } = new();
        public GameObjectType Type { get; internal set; }

        public void Apply(GameObject parent)
        {
            Name = parent.Name;

            Properties = parent.Properties.ToDictionary(entry => entry.Key, entry => entry.Value); // Clone Properties Dictionary
        }

        public T GetValue<T>(string name)
        {
            var lowerName = name.ToLower();

            if (Properties.ContainsKey(lowerName))
            {
                return (T)Properties[lowerName];
            }

            return default;
        }

        public void SetOrAddValue(string name, object value)
        {
            if (Properties.ContainsKey(name.ToLower()))
            {
                Properties[name.ToLower()] = value;
            }
            else
            {
                Properties.Add(name.ToLower(), value);
            }

            OnPropertyChanged?.Invoke(name, value);
        }
    }
}
