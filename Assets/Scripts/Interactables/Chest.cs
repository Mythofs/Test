using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Chest : MonoBehaviour, IInteractable
{
    private bool opened = false;
    private SpriteRenderer sr;
    [SerializeField] private Sprite close;
    [SerializeField] private Sprite open;
    [SerializeField] private LootTable table;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        sr.sprite = close;
    }
    public void Interact()
    {
        if(!opened)
        { 
            InventoryManager.Instance.AddItem(table.getRandomItem());
            opened = !opened;
            sr.sprite = open;
        }
    }

}
