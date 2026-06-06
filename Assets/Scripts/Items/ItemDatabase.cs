using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "ItemDatabase/Create a new ItemDatabase")]
    public class ItemDatabase : ScriptableObject
    {
        [SerializeField] private Dictionary<string, ItemBase> allItems;
        public Dictionary<string, ItemBase> AllItems => allItems;
        public ItemBase GetItemByName(string name) => allItems[name];
    }
}
