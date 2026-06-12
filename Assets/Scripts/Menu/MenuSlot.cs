using Scripts.Player;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Scripts.Menu
{
	public class MenuSlot: Selectable
	{
		[SerializeField] private new string name;
		[SerializeField] private MenuAction action;
		public event Action<MenuSlot> OnSlotSelected;
        public override void OnSelect(BaseEventData eventData)
        {
			base.OnSelect(eventData);
			Player.PlayerInputManager.Instance.Control.UI.Submit.performed += OnSubmit;
			OnSlotSelected?.Invoke(this);
			MenuManager.Instance.SetText(name);
        }
        public override void OnDeselect(BaseEventData eventData)
        {
			base.OnDeselect(eventData);
			Player.PlayerInputManager.Instance.Control.UI.Submit.performed -= OnSubmit;
        }
		private void OnSubmit(InputAction.CallbackContext context)
		{
			Debug.Log(name + " submitted");
			action.Execute();
		}
    }
}