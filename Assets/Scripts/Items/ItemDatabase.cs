using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "ItemDatabase/Create a new ItemDatabase")]
    public class ItemDatabase
    {
        [SerializeField] private List<ItemBase> allItems;
        public List<ItemBase> AllItems => allItems;
    }
}
