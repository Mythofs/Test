using UnityEngine;

namespace Scripts.Items
{
    [System.Serializable]
    public class Item
    {
        [SerializeField] private ItemBase itemBase;
        [SerializeField] private int amount;
        public ItemBase ItemBase => itemBase;
        public int Amount => amount;
        public void SetAmount(int Amount)
        {
            amount = Amount;
        }
        public Item(ItemBase itemBase, int amount)
        {
            this.itemBase = itemBase;
            this.amount = amount;
        }
        public override string ToString()
        {
            return itemBase.ItemName;
        }
    }
}