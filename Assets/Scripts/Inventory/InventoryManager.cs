using NUnit.Framework;
using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public Inventory inventory { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        Load();
        if (inventory == null)
            inventory = new Inventory();
    }
    public void AddItem(Item item)
    {
        inventory.AddItem(item);
        Save();
    }
    public void RemoveItem(Item item)
    {
        inventory.RemoveItem(item);
        Save();
    }
    private void Save()
    {
        string json = JsonUtility.ToJson(inventory, true);
        File.WriteAllText(Application.persistentDataPath + "/inventory.json", json);
    }
    private void Load()
    {
        string path = Application.persistentDataPath + "/inventory.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            inventory = JsonUtility.FromJson<Inventory>(json);
        }
    }
}
