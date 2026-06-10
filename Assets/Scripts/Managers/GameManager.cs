using Scripts.Interactables;
using Scripts.Inventory;
using Scripts.Menu;
using Scripts.Player;
using Scripts.Saving;
using System.Collections.Generic;
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
        public static GameManager Instance { get; private set; }
        public SaveData saveData = new();
        public GameState State { get; private set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
        private void Start()
        {
            SetState(GameState.Overworld);
        }
        public void Save()
        {
            //stores data in savedata
            PlayerController.Instance.Save();
            WorldManager.Instance.Save();
            Inventory.Inventory.Instance.Save();
            //actually saves the data
            SaveManager.Instance.Save(saveData);
        }
        public void Load()
        {
            saveData = SaveManager.Instance.Load();
            PlayerController.Instance.Load(saveData.playerData);
            WorldManager.Instance.Load(saveData.worldData);
            Inventory.Inventory.Instance.Load(saveData.inventoryData);
        }
        public void SetState(GameState state)
        {
            State = state;
            GameStateConfig config = stateConfig[state];
            movement.enabled = config.movementEnabled;
            inventory.enabled = config.inventoryEnabled;
            interact.enabled = config.interactEnabled;
            menu.enabled = config.menuEnabled;
            if(state == GameState.Overworld)
            {
                overworldCam.depth = 0;
                inventoryCam.depth = -1;
                UIManager.Instance.DisableCanvas();
            }
            if (state == GameState.Inventory)
            {
                overworldCam.depth = -1;
                inventoryCam.depth = 0;
                InventoryManager.Instance.Open();
            }
            else if (state == GameState.Menu)
                MenuManager.Instance.Open();
        }
        public enum GameState { Overworld, Interact, Inventory, Menu }
        private readonly Dictionary<GameState, GameStateConfig> stateConfig = new()
        {
            [GameState.Overworld] = new GameStateConfig()
            {
                movementEnabled = true,
                inventoryEnabled = true,
                interactEnabled = true,
                menuEnabled = true,
            },
            [GameState.Inventory] = new GameStateConfig()
            {
                movementEnabled = false,
                inventoryEnabled = true,
                interactEnabled = false,
                menuEnabled = false,
            },
            [GameState.Interact] = new GameStateConfig()
            {
                movementEnabled = false,
                inventoryEnabled = false,
                interactEnabled = true,
                menuEnabled = false,
            },
            [GameState.Menu] = new GameStateConfig()
            {
                movementEnabled = false,
                inventoryEnabled = false,
                interactEnabled = false,
                menuEnabled = true,
            },
        };
        [System.Serializable]
        public struct GameStateConfig
        {
            public bool movementEnabled;
            public bool inventoryEnabled;
            public bool interactEnabled;
            public bool menuEnabled;
        }
    }
}
