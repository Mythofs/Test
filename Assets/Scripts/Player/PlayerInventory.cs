using Scripts.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
	public class PlayerInventory : MonoBehaviour
	{
		private PlayerControl control;
		public static bool InInventory { get; private set; }
		private readonly float delay = 0.2f;
		private float last = 0;
		[SerializeField] Camera overworld;
		[SerializeField] Camera inventory;
		private PlayerMovement playerMovement;
		private PlayerInteract playerInteract;
        private void Awake()
		{
			InInventory = false;
            playerMovement = GetComponent<PlayerMovement>();
            playerInteract = GetComponent<PlayerInteract>();
        }
        private void Start()
        {
            control = PlayerInputManager.Instance.Control;
            OnEnable();
        }
        private void OnEnable()
		{
            if (control == null)
                return;
            control.Player.Inventory.performed += OnInventory;
		}
		private void OnDisable()
		{
            if (control == null)
                return;
            control.Player.Inventory.performed -= OnInventory;
		}
		private void OnInventory(InputAction.CallbackContext context)
		{
			if (last + delay < Time.time)
			{
				last = Time.time;
				InInventory = !InInventory;
				if (InInventory)
				{
					overworld.depth = -1;
					inventory.depth = 0;
					playerMovement.enabled = false;
					playerInteract.enabled = false;
					InventoryManager.Instance.Open();
				}
				else
				{
					overworld.depth = 0;
					inventory.depth = -1;
					playerMovement.enabled = true;
					playerInteract.enabled = true;
				}
			}
		}
	}
}