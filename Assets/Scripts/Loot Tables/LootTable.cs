using Scripts.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.LootTables
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "LootTable", menuName = "Loot Table/Create a new Loot Table")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private List<Item> table;
        [SerializeField] private int id;
        public List<Item> Table => table;
        public int Id => id;
        public Item GetRandomItem()
        {
            if (table != null && table.Count != 0)
            {
                int ran = UnityEngine.Random.Range(0, table.Count);
                return new Item(table[ran].ItemBase, table[ran].Amount);
            }
            return null;
        }
    }
}