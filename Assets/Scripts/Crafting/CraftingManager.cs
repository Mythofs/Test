using Scripts.Inventory;
using Scripts.Items;
using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Crafting
{
    class CraftingManager : MonoBehaviour
    {
        [SerializeField] private CraftingSlot itemPrefab;
        [SerializeField] private RectTransform content;
        [SerializeField] private TextMeshProUGUI mainItemName;
        [SerializeField] private Image mainItemSprite;
        [SerializeField] private List<Image> materialImages;
        [SerializeField] private List<TextMeshProUGUI> materialAmounts;
        [SerializeField] private TextMeshProUGUI mainItemDesc;
        [SerializeField] private CanvasGroup craftingCanvas;
        private CraftingSlot selectedSlot;
        private List<CraftingSlot> craftingSlots = new();
        private HashSet<ItemBase> itemList = new();
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
            ItemBase previous = selectedSlot?.GetItem;
            foreach (CraftingSlot slot in craftingSlots.ToList())
                if (!Inventory.Inventory.Instance.CraftableItems.Contains(slot.GetItem))
                {
                    craftingSlots.Remove(slot);
                    itemList.Remove(slot.GetItem);
                    Destroy(slot.gameObject);
                }
            foreach (ItemBase item in Inventory.Inventory.Instance.CraftableItems)
                if (!itemList.Contains(item))
                {
                    CraftingSlot slot = Instantiate(itemPrefab, content);
                    slot.SetItem(item);
                    craftingSlots.Add(slot);
                    itemList.Add(item);
                }
            if (craftingSlots.FirstOrDefault(slot => slot.GetItem == previous))
                selectedSlot = craftingSlots.FirstOrDefault(slot => slot.GetItem == previous);
            else
                selectedSlot = craftingSlots.FirstOrDefault(); // can still be null if craftingslots is empty
            if (selectedSlot != null)
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
            if (craftingSlots.Count > 0)
                selectedSlot = craftingSlots[0];
            if (selectedSlot != null)
            {
                SetMainCrafting(selectedSlot.GetItem);
                StartCoroutine(Select());
            }
            UIManager.Instance.SetCanvas(craftingCanvas);
        }
        private IEnumerator Select()
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(selectedSlot.gameObject);
        }
    }
}
