using System.Collections.Generic;

namespace Scripts.Inventory
{
    [System.Serializable]
    public struct InventoryData
    {
        public List<ItemSaveData> items;
    }
    [System.Serializable]
    public struct ItemSaveData
    {
        public string itemName;
        public int amount;
    }
}