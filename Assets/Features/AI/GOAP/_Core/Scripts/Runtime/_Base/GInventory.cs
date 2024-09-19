using System.Collections.Generic;
using UnityEngine;

namespace Project.AI.GOAP
{
    public class GInventory
    {
        private List<GameObject> _items = new();

        public void AddItem(GameObject item)
        {
            _items.Add(item);
        }

        public GameObject FindItemWithTag(string tag)
        {
            foreach (GameObject item in _items)
                if (item.CompareTag(tag)) return item;
            return null;
        }

        public bool RemoveItem(GameObject item)
        {
            return _items.Remove(item);
        }
    }
}
