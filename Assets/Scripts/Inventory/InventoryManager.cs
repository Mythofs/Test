using Scripts.Items;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Scripts.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private List<InventorySlot> inventorySlots;
        [SerializeField] private TextMeshProUGUI sideNameText;
        [SerializeField] private Image sideImage;
        [SerializeField] private TextMeshProUGUI sideDescText;
        [SerializeField] private ItemDatabase allItems;
        private InventorySlot selectedSlot;
        public static InventoryManager Instance { get; private set; }
        public Inventory Inventory { get; private set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            Load();
            Inventory = new Inventory(allItems, 50);
        }
        private void Start()
        {
            foreach (InventorySlot slot in inventorySlots)
                slot.OnSlotSelected += slot => selectedSlot = slot;
            Display();
        }
        public Item AddItem(Item item)
        {
            Item leftover = Inventory.AddItem(item);
            Save();
            Display();
            return leftover;
        }
        public void RemoveItem(Item item)
        {
            Inventory.RemoveItem(item);
            Save();
            Display();
        }
        private void Save()
        {
            string json = JsonUtility.ToJson(Inventory, true);
            File.WriteAllText(Application.persistentDataPath + "/inventory.json", json);
        }
        private void Load()
        {
            string path = Application.persistentDataPath + "/inventory.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Inventory = JsonUtility.FromJson<Inventory>(json);
            }
        }
        private void Display()
        {
            int index = 0;
            foreach (InventorySlot slot in inventorySlots)
            {
                if (index >= Inventory.Count())
                    slot.ClearItem();
                else
                    slot.SetItem(Inventory.GetItem(index));
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
    }
}