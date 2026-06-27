using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "ItemDatabase/Create a new ItemDatabase")]
    public class ItemDatabase : ScriptableObject
    {
        [SerializeField] private List<ItemBase> allItemsList;
        private Dictionary<string, ItemBase> allItems;
        private void OnEnable()
        {
            if (allItems == null)
            {
                allItems = new();
                foreach (ItemBase item in allItemsList)
                    allItems.Add(item.ItemName, item);
            }
        }
        public Dictionary<string, ItemBase> AllItems => allItems;
        public ItemBase GetItemByName(string name) => allItems[name];
    }
}