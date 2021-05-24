using System;
using System.Collections.Generic;
using System.Linq;

namespace GrandTextAdventure.Core
{
    public class Inventory
    {
        private readonly Dictionary<GameObject, int> _items = new();

        public void Add(GameObject gameObject)
        {
            if (_items.ContainsKey(gameObject))
            {
                _items[gameObject]++;
            }
            else
            {
                _items.Add(gameObject, 1);
            }
        }

        public void Add(GameObject gameObject, int amount)
        {
            if (_items.ContainsKey(gameObject))
            {
                _items[gameObject] += amount;
            }
            else
            {
                _items.Add(gameObject, amount);
            }
        }

        public void Remove(GameObject gameObject)
        {
            if (_items.ContainsKey(gameObject))
            {
                if (_items[gameObject] >= 1)
                {
                    _items[gameObject] -= 1;

                    if (_items[gameObject] == 0)
                    {
                        _items.Remove(gameObject);
                    }
                }
            }
        }

        public void Remove(GameObject gameObject, int amount)
        {
            if (_items.ContainsKey(gameObject))
            {
                if (_items[gameObject] >= amount)
                {
                    _items[gameObject] -= amount;

                    if (_items[gameObject] <= 0)
                    {
                        _items.Remove(gameObject);
                    }
                }
            }
        }

        public void Remove(string objectName)
        {
            var gameObject = _items.Keys.FirstOrDefault(_ => _.Name == objectName);

            if (gameObject != null)
            {
                Remove(gameObject);
            }
        }

        public void Transfer(Inventory inventory)
        {
            if (_items.Count > 0)
            {
                foreach (var item in _items)
                {
                    inventory.Add(item.Key, item.Value);
                }

                Console.WriteLine("Transfered Items to your Inventory");
            }
        }
    }
}
