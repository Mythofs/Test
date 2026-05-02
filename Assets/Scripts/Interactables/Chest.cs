using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Chest : MonoBehaviour, IInteractable
{
    private bool opened = false;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite close;
    [SerializeField] private Sprite open;
    [SerializeField] private LootTable table;
    [SerializeField] private TextMeshProUGUI text;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = close;
        text.enabled = false;
    }
    public void Interact()
    {
        if(!opened)
        {
            Item item = table.getRandomItem();
            InventoryManager.Instance.AddItem(item);
            opened = !opened;
            spriteRenderer.sprite = open;
            text.SetText("You recieved " + item.Amount + " " + item.ItemBase.ItemName);
            text.enabled = true;
        }
    }
    public void CloseDialog()
    {
        if(text.enabled)
        {
            text.enabled = false;
        }
    }
}
