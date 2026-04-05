using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        inventory ??= new Inventory();
        Display();
    }
    public void AddItem(Item item)
    {
        inventory.AddItem(item);
        Save();
        Display();
    }
    public void RemoveItem(Item item)
    {
        inventory.RemoveItem(item);
        Save();
        Display();
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
    private void Display()
    {
        int index = 0;
        foreach(Transform child in transform)
        {
            if (index >= inventory.Count())
                break;
            Transform[] componenets = child.GetComponentsInChildren<Transform>().ToArray();
            componenets[1].GetComponent<Image>().sprite = inventory.GetItem(index).Base.ItemSprite;
            componenets[2].GetComponent<TextMeshProUGUI>().SetText("" + inventory.GetItem(index).Amount);
        }
    }
}
