using Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
	public class PlayerMenu : MonoBehaviour
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
			control.Player.Menu.performed += OnMenu;
		}
		private void OnDisable()
		{
			if (control == null)
				return;
			control.Player.Menu.performed -= OnMenu;
		}
		private void OnMenu(InputAction.CallbackContext context)
		{
			if(last + delay <Time.time)
			{
				last = Time.time;
				if (GameManager.Instance.State == GameManager.GameState.Overworld)
					GameManager.Instance.SetState(GameManager.GameState.Menu);
				else
					GameManager.Instance.SetState(GameManager.GameState.Overworld);
			}
		}
	}
}