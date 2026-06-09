using Scripts.Crafting;
using Scripts.Items;
using Scripts.Managers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private ItemDatabase itemDatabase;
        private List<Item> ItemList;
        public List<ItemBase> CraftableItems { get; private set; }
        private Dictionary<ItemBase, int> totalItems = new();
        private int Capacity;
        public static Inventory Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
            ItemList = new();
            CraftableItems = new();
            Capacity = 20;
        }
        //probably should just change to have only one return statement at the end
        public Item AddItem(Item item)
        {
            foreach (var item1 in ItemList)
                if (item1.ItemBase == item.ItemBase && item.Amount + item1.Amount > item.ItemBase.MaxStack)
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
                        InventoryManager.Instance.Display();
                        return item;
                    }
                }
            if (ItemList.Count == Capacity)
            {
                UpdateCraftable();
                InventoryManager.Instance.Display();

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
                InventoryManager.Instance.Display();
                return item;
            }
            UpdateCraftable();
            InventoryManager.Instance.Display();
            return new Item(item.ItemBase, 0);
        }
        public void RemoveItem(Item item)
        {
            for (int a = ItemList.Count - 1; a >= 0; a++)
            {
                Item item1 = ItemList[a];
                if (item1.ItemBase == item.ItemBase)
                {
                    item1.SetAmount(item1.Amount - item.Amount);
                    if (item1.Amount <= 0)
                        ItemList.Remove(item1);
                    break;
                }
            }
            InventoryManager.Instance.Display();
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
            //updates total items, probably change later, b/c this is inefficient, should just change in additem
            foreach (Item item in ItemList)
                if (totalItems.ContainsKey(item.ItemBase))
                    totalItems[item.ItemBase] += item.Amount;
                else
                    totalItems.Add(item.ItemBase, item.Amount);
            foreach (ItemBase itemBase in itemDatabase.AllItems.Values)
                if (!CraftableItems.Contains(itemBase) && CanCraft(itemBase))
                    CraftableItems.Add(itemBase);
            CraftingManager.Instance.SetContent();
        }
        private bool CanCraft(ItemBase itemBase)
        {
            if (itemBase.CraftingRecipe.Count == 0)
                return false;
            foreach (Item ingredient in itemBase.CraftingRecipe)
                if (totalItems.ContainsKey(ingredient.ItemBase))
                {
                    if (totalItems[ingredient.ItemBase] < ingredient.Amount)
                        return false;
                }
                else
                    return false;
            return true;
        }
        public void Save()
        {
            List<ItemSaveData> dataList = new();
            foreach (Item item in ItemList)
                dataList.Add(new ItemSaveData { itemName = item.ItemBase.ItemName, amount = item.Amount });
            GameManager.Instance.saveData.inventoryData.items = dataList;
        }
        public void Load(InventoryData inventoryData)
        {
            ItemList.Clear();
            foreach (ItemSaveData data in inventoryData.items)
                ItemList.Add(new Item(itemDatabase.GetItemByName(data.itemName), data.amount));
            UpdateCraftable();
        }
        public void Craft(ItemBase item)
        {
            if (!CraftableItems.Contains(item))
                return;
            int usedStacks = 0;
            foreach (Item ingredient in item.CraftingRecipe)
            {
                int amount = totalItems[ingredient.ItemBase];
                if (amount / ingredient.ItemBase.MaxStack < (amount - ingredient.Amount) / ingredient.ItemBase.MaxStack)
                    usedStacks++;
            }
            if(ItemList.Count + Math.Max(item.CraftedAmount / item.MaxStack, 1) - usedStacks <= Capacity)
            {
                foreach (Item ingredient in item.CraftingRecipe)
                    RemoveItem(ingredient);
                AddItem(new Item(item, item.CraftedAmount));
            }
            Display();
        }
    }
}