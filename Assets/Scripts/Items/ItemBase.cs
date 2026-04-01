using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Create a new Item")]
public class ItemBase : ScriptableObject
{
    [SerializeField] string itemName;
    [TextArea] [SerializeField] string desc;
    [SerializeField] Sprite itemSprite;
    [SerializeField] int maxStack = 1;
    public string ItemName => itemName;
    public string Desc => desc;
    public Sprite ItemSprite => itemSprite;
    public int MaxStack => maxStack;
}
