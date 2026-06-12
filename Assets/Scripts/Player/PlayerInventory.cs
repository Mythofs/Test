using Scripts.Managers;
using Scripts.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
	public class PlayerInventory : MonoBehaviour
	{
		private PlayerControl control;
		private readonly float delay = 0.1f;
		private float last = 0;
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
				if (GameManager.Instance.State == GameManager.GameState.Overworld)
					GameManager.Instance.SetState(GameManager.GameState.Inventory);
            }
        }
	}
}