using Scripts.Inventory;
using Scripts.Items;
using Scripts.LootTables;
using UnityEngine;

namespace Scripts.Interactables
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Chest : Interactable
    {
        [SerializeField] private Sprite close;
        [SerializeField] private Sprite open;
        [SerializeField] private LootTable table;
        private bool opened = false;
        private bool cancel = false;
        protected override void Awake()
        {
            base.Awake();
            spriteRenderer.sprite = close;
        }
        public override void Interact()
        {
            opened = !opened;
            Item item = table.GetRandomItem();
            int amount = item.Amount;
            Item leftover = InventoryManager.Instance.AddItem(item);
            spriteRenderer.sprite = open;
            StartCoroutine(DialogBox.Instance.DisplayText("You recieved " + (amount - leftover.Amount) + " " + item.ItemBase.ItemName));
            cancel = true;
        }
        public override bool CanInteract() => !opened;
        public override bool CanCancel()
        {
            if (DialogBox.Instance.Displaying)
                return false;
            return cancel;
        }
    }
}
