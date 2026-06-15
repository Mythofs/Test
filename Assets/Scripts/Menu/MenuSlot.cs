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
			action.Execute();
		}
        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (Player.PlayerInputManager.Instance != null)
                Player.PlayerInputManager.Instance.Control.UI.Submit.performed -= OnSubmit;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            if (Player.PlayerInputManager.Instance != null)
                Player.PlayerInputManager.Instance.Control.UI.Submit.performed -= OnSubmit;
        }
    }
}