using UnityEngine.Rendering;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Create a new Item")]
public class ItemBase : ScriptableObject
{
    [SerializeField] private int itemId;
    [SerializeField] private string itemName;
    [TextArea][SerializeField] private string desc;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private int maxStack = 1;
    public int ItemId => itemId;
    public string ItemName => itemName;
    public string Desc => desc;
    public Sprite ItemSprite => itemSprite;
    public int MaxStack => maxStack;
}

[System.Serializable]
public class Item
{
    public ItemBase Base { get; set; }
    public int Amount { get; set; }
    public Item(ItemBase ib, int amount)
    {
        Base = ib;
        Amount = amount;
    }
}
[System.Serializable]
public class Inventory
{
    public List<Item> inventory { get; set; }
    public int capacity { get; set; }
    public Inventory()
    {
        inventory = new();
    }
    public void AddItem(Item item)
    {
        foreach (var item1 in inventory)
            if (item1.Base == item.Base)
            {
                if (item1.Amount + item.Amount > item1.Base.MaxStack)
                {
                    item.Amount = item.Amount + item1.Amount - item1.Base.MaxStack;
                    item1.Amount = item1.Base.MaxStack;
                    if (inventory.Count < capacity)
                        inventory.Add(item);
                }
                else
                    item1.Amount += item.Amount;
                break;
            }
        if(inventory.Count < capacity)
            inventory.Add(item);
    }
    public void RemoveItem(Item item)
    {
        foreach(var item1 in inventory)
            if(item1.Base == item.Base)
            {
                item1.Amount -= item.Amount;
                if (item1.Amount <= 0)
                    inventory.Remove(item1);
                break;
            }
    }
    public int Count()
    {
        return inventory.Count;
    }
    public Item GetItem(int index)
    {
        if (index < 0 || index >= inventory.Count)
            return null;
        return inventory[index];
    }
}