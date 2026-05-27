using Scripts.Items;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Crafting
{
    class CraftingSlot : Selectable, ISubmitHandler
    {
        [SerializeField] private Image highlight;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private Image itemSprite;
        private ItemBase itemBase = null;
        public event Action<CraftingSlot> OnSlotSelected;
        public ItemBase GetItem => itemBase;
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
            CraftingManager.Instance.SetMainCrafting(itemBase);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            highlight.enabled = false;
        }
        public void OnSubmit(BaseEventData eventData)
        {
            Debug.Log("Crafting " + itemBase.ItemName);
        }
        public void SetItem(ItemBase item)
        {
            this.itemBase = item;
            itemSprite.enabled = true;
            itemSprite.sprite = item.ItemSprite;
            itemName.SetText(item.ItemName);
        }
        public void ClearItem()
        {
            itemBase = null;
            itemSprite.enabled = false;
            itemName.SetText("");  
        }
    }
}
