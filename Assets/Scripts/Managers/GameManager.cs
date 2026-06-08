using Scripts.Interactables;
using Scripts.Inventory;
using Scripts.Menu;
using Scripts.Player;
using Scripts.Saving;
using UnityEngine;

namespace Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private PlayerInteract interact;
        [SerializeField] private PlayerMenu menu;
        [SerializeField] private Camera overworldCam;
        [SerializeField] private Camera inventoryCam;
        [SerializeField] private Canvas menuCanvas;
        public static GameManager Instance { get; private set; }
        public SaveData saveData = new();
        public GameState State { get; private set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
            SetState(GameState.Overworld);
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
        public void SetState(GameState state)
        {
            State = state;
            if(state == GameState.Overworld)
            {
                movement.enabled = true;
                inventory.enabled = true;
                interact.enabled = true;
                menu.enabled = true;
                overworldCam.depth = 0;
                inventoryCam.depth = -1;
                menuCanvas.enabled = false;
            }
            else if(state == GameState.Inventory)
            {
                movement.enabled = false;
                inventory.enabled = true;
                interact.enabled = false;
                menu.enabled = false;
                overworldCam.depth = -1;
                inventoryCam.depth = 0;
                menuCanvas.enabled = false;
                InventoryManager.Instance.Open();
            }
            else if(state == GameState.Interact)
            {
                movement.enabled = false;
                inventory.enabled = false;
                interact.enabled = true;
                menu.enabled = false;
                menuCanvas.enabled = false;
            }
            else if(state == GameState.Menu)
            {
                movement.enabled = false;
                inventory.enabled = false;
                interact.enabled = false;
                menu.enabled = false;
                menuCanvas.enabled = true;
                MenuManager.Instance.Open();
            }
        }
        public enum GameState { Overworld, Interact, Inventory, Menu }
    }
}
