using Scripts.Player;
using Scripts.Saving;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SaveData saveData = new();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    public void Save()
    {
        PlayerController.Instance.Save();
        SaveManager.Instance.Save(saveData);
    }
    public void Load()
    {
        saveData = SaveManager.Instance.Load();
    }
}
