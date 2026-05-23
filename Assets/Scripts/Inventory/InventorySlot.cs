using Scripts.Items;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Inventory
{
    class InventorySlot : Selectable
    {
        [SerializeField] private Image highlight;
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] private Image itemSprite;
        private Item item = null;
        public event Action<InventorySlot> OnSlotSelected;
        public Item GetItem() => item;
        protected override void Awake()
        {
            base.Awake();
            highlight.enabled = false;
            ClearItem();
        }
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            highlight.enabled = true;
            OnSlotSelected?.Invoke(this);
            InventoryManager.Instance.SetSideBar();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            highlight.enabled = false;
        }
        public void SetItem(Item item)
        {
            this.item = item;
            amountText.enabled = true;
            itemSprite.enabled = true;
            amountText.SetText("" + item.Amount);
            itemSprite.sprite = item.ItemBase.ItemSprite;
        }
        public void ClearItem()
        {
            item = null;
            amountText.SetText("");
            amountText.enabled = false;
            itemSprite.enabled = false;
        }
    }
}
