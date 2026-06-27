using Scripts.Items;
using Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI sideNameText;
        [SerializeField] private Image sideImage;
        [SerializeField] private TextMeshProUGUI sideDescText;
        [SerializeField] private CanvasGroup inventoryCanvas;
        private InventorySlot selectedSlot;
        private InventorySlot[] inventorySlots;
        public static InventoryManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        private void Start()
        {
            inventorySlots = GetComponentsInChildren<InventorySlot>();
            foreach (InventorySlot slot in inventorySlots)
                slot.OnSlotSelected += slot => selectedSlot = slot;
            Display();
        }
        public void Display()
        {
            int index = 0;
            foreach (InventorySlot slot in inventorySlots)
            {
                if (index >= Inventory.Instance.Count())
                    slot.ClearItem();
                else
                    slot.SetItem(Inventory.Instance.GetItem(index));
                index++;
            }
            SetSideBar();
        }
        public void SetSideBar()
        {
            if (selectedSlot != null)
            {
                Item current = selectedSlot.GetItem();
                if (current != null)
                {
                    sideNameText.SetText(current.ItemBase.ItemName);
                    sideImage.enabled = true;
                    sideImage.sprite = current.ItemBase.ItemSprite;
                    sideDescText.SetText(current.ItemBase.Desc);
                }
                else
                {
                    sideNameText.SetText("");
                    sideImage.enabled = false;
                    sideDescText.SetText("");
                }
            }
            else
            {
                sideNameText.SetText("");
                sideImage.enabled = false;
                sideDescText.SetText("");
            }
        }
        public void Open()
        {
            Display();
            if (selectedSlot != null)
                EventSystem.current.SetSelectedGameObject(selectedSlot.gameObject);
            else if (inventorySlots.Length > 0)
            {
                EventSystem.current.SetSelectedGameObject(inventorySlots[0].gameObject);
                selectedSlot = inventorySlots[0];
            }
            UIManager.Instance.SetCanvas(inventoryCanvas);
        }
    }
}