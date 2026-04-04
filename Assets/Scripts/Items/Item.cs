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
    private List<Item> inventory;
    public Inventory()
    {
        inventory = new();
    }
    public List<Item> getInventory()
    {
        return inventory;
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
                }
                else
                    item1.Amount += item.Amount;
                break;
            }
        inventory.Add(item);
    }
    public void RemoveItem(Item item)
    {

    }
}