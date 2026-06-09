using Scripts.Inventory;
using Scripts.Items;
using Scripts.LootTables;
using UnityEngine;

namespace Scripts.Interactables
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Chest : IInteractable, ISaveable
    {
        [SerializeField] private Sprite close;
        [SerializeField] private Sprite open;
        [SerializeField] private LootTable table;
        [SerializeField] public string id;
        private bool opened = false;
        private bool cancel = false;
        public string Id => id;
        protected override void Awake()
        {
            base.Awake();
            spriteRenderer.sprite = close;
        }
        public override void Interact()
        {
            if (!opened)
            {
                opened = true;
                Item item = table.GetRandomItem();
                int amount = item.Amount;
                Item leftover = Inventory.Inventory.Instance.AddItem(item);
                spriteRenderer.sprite = open;
                StartCoroutine(DialogBox.Instance.DisplayText("You recieved " + (amount - leftover.Amount) + " " + item.ItemBase.ItemName));
                cancel = true;
            }
        }
        public override bool CanInteract() => !opened;
        public override bool CanCancel()
        {
            if (DialogBox.Instance.Displaying)
                return false;
            return cancel;
        }
        public string Serialize()
        {
            ChestData data = new();
            data.opened = opened;
            return JsonUtility.ToJson(data, true);
        }
        public void Deserialize(string data)
        {
            ChestData chestData = JsonUtility.FromJson<ChestData>(data);
            opened = chestData.opened;
            if (opened)
                spriteRenderer.sprite = close;
            else
                spriteRenderer.sprite = open;
        }
        private class ChestData { public bool opened; }
    }
}
