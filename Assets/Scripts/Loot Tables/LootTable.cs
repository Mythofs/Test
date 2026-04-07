using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "LootTable", menuName = "Loot Table/Create a new Loot Table")]
public class LootTable : ScriptableObject
{
    [SerializeField] List<Item> table;
    [SerializeField] private int id;
    public List<Item> Table => table;
    public int Id => id;
    public Item getRandomItem()
    {
        if (table != null && table.Count != 0)
        {
            int ran = UnityEngine.Random.Range(0, table.Count);
            return table[ran];
        }
        return null;
    }
}