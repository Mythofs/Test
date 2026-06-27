using Scripts.Interactables;
using Scripts.Inventory;
using Scripts.Menu;
using Scripts.Player;
using Scripts.Saving;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Camera overworldCam;
        [SerializeField] private Camera inventoryCam;
        [SerializeField] private Camera battleCam;
        public static GameManager Instance { get; private set; }
        public SaveData saveData = new();
        public GameState State { get; private set; }
        public bool StateChangedThisFrame { get; private set; }
        private PlayerControl control;
        public int Level { get; set; }
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
            Level = 0;
        }
        private void Start()
        {
            Load();
            control = Player.PlayerInputManager.Instance.Control;
            SetState(GameState.Overworld);
        }
        private void LateUpdate()
        {
            StateChangedThisFrame = false;
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
            if (saveData != null)
            {
                PlayerController.Instance.Load(saveData.playerData);
                WorldManager.Instance.Load(saveData.worldData);
                Inventory.Inventory.Instance.Load(saveData.inventoryData);
            }
        }
        public void SetState(GameState state)
        {
            State = state;
            GameStateConfig config = stateConfig[state];
            SetEnabled(control.Player.Move, config.movementEnabled);
            SetEnabled(control.Player.Inventory, config.inventoryEnabled);
            SetEnabled(control.Player.Interact, config.interactEnabled);
            SetEnabled(control.Player.Menu, config.menuEnabled);
            if (config.UIEnabled) control.UI.Enable();
            else control.UI.Disable();
            if (state == GameState.Overworld)
            {
                overworldCam.depth = 0;
                inventoryCam.depth = -1;
                battleCam.depth = -1;
                UIManager.Instance.DisableCanvas();
            }
            else if (state == GameState.Inventory)
            {
                overworldCam.depth = -1;
                inventoryCam.depth = 0;
                battleCam.depth = -1;
                InventoryManager.Instance.Open();
            }
            else if (state == GameState.Menu)
                MenuManager.Instance.Open();
            else if (state == GameState.Battle)
            {
                overworldCam.depth = -1;
                inventoryCam.depth = -1;
                battleCam.depth = 0;
                BattleManager.Instance.Open();
            }
            StateChangedThisFrame = true;
        }
        private void SetEnabled(InputAction action, bool enabled)
        {
            if (enabled) action.Enable();
            else action.Disable();
        }
        public enum GameState { Overworld, Interact, Inventory, Menu, Battle }
        private readonly Dictionary<GameState, GameStateConfig> stateConfig = new()
        {
            [GameState.Overworld] = new GameStateConfig()
            {
                movementEnabled = true,
                inventoryEnabled = true,
                interactEnabled = true,
                menuEnabled = true,
                UIEnabled = false,
            },
            [GameState.Inventory] = new GameStateConfig()
            {
                movementEnabled = false,
                inventoryEnabled = true,
                interactEnabled = false,
                menuEnabled = false,
                UIEnabled = true,
            },
            [GameState.Interact] = new GameStateConfig()
            {
                movementEnabled = false,
                inventoryEnabled = false,
                interactEnabled = true,
                menuEnabled = false,
                UIEnabled = true,
            },
            [GameState.Menu] = new GameStateConfig()
            {
                movementEnabled = false,
                inventoryEnabled = false,
                interactEnabled = false,
                menuEnabled = true,
                UIEnabled = true,
            },
            [GameState.Battle] = new GameStateConfig()
            {
                movementEnabled = false,
                inventoryEnabled = false,
                interactEnabled = false,
                menuEnabled = false,
                UIEnabled = true,
            },
        };
        [System.Serializable]
        public struct GameStateConfig
        {
            public bool movementEnabled;
            public bool inventoryEnabled;
            public bool interactEnabled;
            public bool menuEnabled;
            public bool UIEnabled;
        }
    }
}
