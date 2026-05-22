using UnityEngine;
using System.Collections.Generic;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item/Create a new Item")]
    public class ItemBase : ScriptableObject
    {
        [SerializeField] private int itemId;
        [SerializeField] private string itemName;
        [TextArea][SerializeField] private string desc;
        [SerializeField] private Sprite itemSprite;
        [SerializeField] private int maxStack = 1;
        [SerializeField] private List<Item> craftingRecipe;
        [SerializeField] private int craftedAmount;
        public int ItemId => itemId;
        public string ItemName => itemName;
        public string Desc => desc;
        public Sprite ItemSprite => itemSprite;
        public int MaxStack => maxStack;
        public List<Item> CraftingRecipe => craftingRecipe;
        public int CraftedAmount => CraftedAmount;
    }
}