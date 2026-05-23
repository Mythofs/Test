using Scripts.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inventory
{
    [System.Serializable]
    public class Inventory
    {
        private readonly ItemDatabase itemDatabase;
        public List<Item> ItemList { get; private set; }
        public List<ItemBase> CraftableItems { get; private set; }
        public int Capacity { get; private set; }
        public Inventory(ItemDatabase itemdb, int capacity)
        {
            ItemList = new();
            CraftableItems = new();
            Capacity = capacity;
            itemDatabase = itemdb;
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
                        {
                            UpdateCraftable();
                            return item;
                        }
                    }
                }
            if (ItemList.Count == Capacity)
            {
                UpdateCraftable();
                return item;
            }
            if (item.Amount > item.ItemBase.MaxStack)
                while (item.Amount > item.ItemBase.MaxStack && ItemList.Count < Capacity)
                {
                    ItemList.Add(new Item(item.ItemBase, item.ItemBase.MaxStack));
                    item.SetAmount(item.Amount - item.ItemBase.MaxStack);
                }
            if (ItemList.Count < Capacity)
                ItemList.Add(item);
            else
            {
                UpdateCraftable();
                return item;
            }
            UpdateCraftable();
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
        private void UpdateCraftable()
        {
            foreach(ItemBase itemBase in itemDatabase.AllItems)
            {
                if (!CraftableItems.Contains(itemBase) && CanCraft(itemBase))
                    CraftableItems.Add(itemBase);
            }
        }
        private bool CanCraft(ItemBase itemBase)
        {
            if (itemBase.CraftingRecipe == null)
                return false;
            int i = itemBase.CraftingRecipe.Count;
            foreach(Item ingredient in itemBase.CraftingRecipe)
            {
                foreach (Item item in ItemList)
                    if (ingredient.ItemBase.ItemName == item.ItemBase.ItemName)
                        if (ingredient.Amount > item.Amount)
                            return false;
                        else
                            i--;
            }
            return i == 0;
        }
    }
}