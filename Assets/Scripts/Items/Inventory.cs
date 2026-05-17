using System.Collections.Generic;

namespace Scripts.Items
{
    [System.Serializable]
    public class Inventory
    {
        public List<Item> ItemList { get; set; }
        public int Capacity { get; set; }
        public Inventory()
        {
            ItemList = new();
            Capacity = 50;
        }
        //returns amount of item left over
        public Item AddItem(Item item)
        {
            foreach (var item1 in ItemList)
                if (item1.ItemBase == item.ItemBase)
                {
                    //if items > maxstack, update current stack, then add stacks
                    if (item.Amount + item1.Amount > item.ItemBase.MaxStack)
                    {
                        item1.SetAmount(item.ItemBase.MaxStack);
                        item.SetAmount(item.Amount + item1.Amount - item.ItemBase.MaxStack);
                        while (item.Amount > item.ItemBase.MaxStack && ItemList.Count < Capacity)
                        {
                            ItemList.Add(new Item(item.ItemBase, item.ItemBase.MaxStack));
                            item.SetAmount(item.Amount - item.ItemBase.MaxStack);
                        }
                        if (ItemList.Count < Capacity)
                            ItemList.Add(item);
                        else
                            return item;
                    }
                }
            if (ItemList.Count == Capacity)
                return item;
            if (item.Amount > item.ItemBase.MaxStack)
                while (item.Amount > item.ItemBase.MaxStack && ItemList.Count < Capacity)
                {
                    ItemList.Add(new Item(item.ItemBase, item.ItemBase.MaxStack));
                    item.SetAmount(item.Amount - item.ItemBase.MaxStack);
                }
            if (ItemList.Count < Capacity)
                ItemList.Add(item);
            else
                return item;
            return new Item(item.ItemBase, 0);
        }
        public void RemoveItem(Item item)
        {
            foreach (var item1 in ItemList)
                if (item1.ItemBase == item.ItemBase)
                {
                    item1.SetAmount(item1.Amount - item.Amount);
                    if (item1.Amount <= 0)
                        ItemList.Remove(item1);
                    break;
                }
        }
        public int Count()
        {
            return ItemList.Count;
        }
        public Item GetItem(int index)
        {
            if (index < 0 || index >= ItemList.Count)
                return null;
            return ItemList[index];
        }
    }
}