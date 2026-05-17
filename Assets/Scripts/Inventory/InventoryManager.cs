using Scripts.Items;
using System.Linq;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }
        public Scripts.Items.Inventory Inventory { get; private set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            Load();
            Inventory ??= new Scripts.Items.Inventory();
            Inventory.Capacity = 50;
        }
        private void Start()
        {
            Display();
        }
        public Item AddItem(Item item)
        {
            Item leftover = Inventory.AddItem(item);
            Save();
            Display();
            InventoryMovement.Instance.SetSideBar();
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
                Inventory = JsonUtility.FromJson<Scripts.Items.Inventory>(json);
            }
        }
        private void Display()
        {
            int index = 0;
            foreach (Transform child in transform)
            {
                Transform[] componenets = child.GetComponentsInChildren<Transform>().ToArray();
                if (index >= Inventory.Count())
                {
                    componenets[1].GetComponent<Image>().enabled = false;
                    componenets[2].GetComponent<TextMeshProUGUI>().enabled = false;
                }
                else
                {
                    componenets[1].GetComponent<Image>().enabled = true;
                    componenets[2].GetComponent<TextMeshProUGUI>().enabled = true;
                    componenets[1].GetComponent<Image>().sprite = Inventory.GetItem(index).ItemBase.ItemSprite;
                    componenets[2].GetComponent<TextMeshProUGUI>().SetText("" + Inventory.GetItem(index).Amount);
                }
                index++;
            }
        }
    }
}