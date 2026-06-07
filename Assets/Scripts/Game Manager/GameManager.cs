using Scripts.Interactables;
using Scripts.Player;
using Scripts.Saving;
using UnityEngine;

namespace Scripts.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public SaveData saveData = new();
        public GameState state = GameState.Overworld;
        //move all logic for disabling movement/opening inventory/crafting ui event system to gamemanager
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
        public void Save()
        {
            //stores data in savedata
            PlayerController.Instance.Save();
            WorldManager.Instance.Save();
            Scripts.Inventory.Inventory.Instance.Save();
            //actually saves the data
            SaveManager.Instance.Save(saveData);
        }
        public void Load()
        {
            saveData = SaveManager.Instance.Load();
            PlayerController.Instance.Load(saveData.playerData);
            WorldManager.Instance.Load(saveData.worldData);
            Scripts.Inventory.Inventory.Instance.Load(saveData.inventoryData);
        }
        public enum GameState { Overworld, Interact, Inventory }
    }
}
