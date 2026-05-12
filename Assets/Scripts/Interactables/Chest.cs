using Scripts.Inventory;
using Scripts.Items;
using Scripts.LootTables;
using UnityEngine;

namespace Scripts.Interactables
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Chest : Interactable
    {
        private bool opened = false;
        [SerializeField] private Sprite close;
        [SerializeField] private Sprite open;
        [SerializeField] private LootTable table;
        protected override void Awake()
        {
            base.Awake();
            spriteRenderer.sprite = close;
        }
        public override void Interact()
        {
            if (!opened)
            {
                Item item = table.getRandomItem();
                int amount = item.Amount;
                Item leftover = InventoryManager.Instance.AddItem(item);
                opened = !opened;
                spriteRenderer.sprite = open;
                StartCoroutine(DialogBox.Instance.DisplayText("You recieved " + (amount - leftover.Amount) + " " + item.ItemBase.ItemName));
            }
        }
        public override bool CanInteract()
        {
            return !opened;
        }
    }
}
