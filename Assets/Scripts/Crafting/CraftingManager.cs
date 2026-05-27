using Scripts.Inventory;
using Scripts.Items;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Crafting
{
    class CraftingManager : MonoBehaviour
    {
        [SerializeField] private CraftingSlot itemPrefab;
        [SerializeField] private ScrollRect scroll;
        [SerializeField] private TextMeshProUGUI mainItemName;
        [SerializeField] private Image mainItemSprite;
        [SerializeField] private List<Image> materialImages;
        [SerializeField] private List<TextMeshProUGUI> materialAmounts;
        [SerializeField] private TextMeshProUGUI mainItemDesc;
        private CraftingSlot selectedSlot;
        private List<CraftingSlot> craftingSlots = new();
        public static CraftingManager Instance;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            ClearMainCrafting();
            SetContent();
            foreach (CraftingSlot slot in craftingSlots)
                slot.OnSlotSelected += slot => selectedSlot = slot;
        }
        public void SetContent()
        {
            foreach (Transform slot in scroll.content)
                Destroy(slot.gameObject);
            foreach (ItemBase item in Scripts.Inventory.Inventory.Instance.CraftableItems)
            {
                CraftingSlot slot = Instantiate(itemPrefab, scroll.content);
                slot.SetItem(item);
                craftingSlots.Add(slot);
            }
            if(selectedSlot != null)
                SetMainCrafting(selectedSlot.GetItem);
        }
        public void SetMainCrafting(ItemBase itemBase)
        {
            mainItemName.SetText(itemBase.ItemName);
            mainItemDesc.SetText(itemBase.Desc);
            mainItemSprite.enabled = true;
            mainItemSprite.sprite = itemBase.ItemSprite;
            for(int a = 0; a < materialImages.Count; a++)
            {
                if (a < itemBase.CraftingRecipe.Count)
                {
                    materialImages[a].enabled = true;
                    materialImages[a].sprite = itemBase.CraftingRecipe[a].ItemBase.ItemSprite;
                    materialAmounts[a].SetText("" + itemBase.CraftingRecipe[a].Amount);
                }
                else
                {
                    materialImages[a].enabled = false;
                    materialAmounts[a].SetText("");
                }
            }
        }
        public void ClearMainCrafting()
        {
            mainItemName.SetText("");
            mainItemDesc.SetText("");
            mainItemSprite.enabled = false;
            for(int a = 0; a < materialImages.Count; a++)
            {
                materialImages[a].enabled = false;
                materialAmounts[a].SetText("");
            }
        }
        public void Open()
        {
            Debug.Log("Crafting Manager Opening ...");
            if (selectedSlot != null)
                EventSystem.current.SetSelectedGameObject(selectedSlot.gameObject);
            else if (craftingSlots.Count > 0)
            {
                EventSystem.current.SetSelectedGameObject(craftingSlots[0].gameObject);
                Debug.Log("Crafting Manager Setting selected");
            }
        }
    }
}
