using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Menu
{
	public class MenuSlot: Selectable, ISubmitHandler
	{
		[SerializeField] private new string name;
		[SerializeField] private MenuAction action;
		public event Action<MenuSlot> OnSlotSelected;
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
			OnSlotSelected?.Invoke(this);
			MenuManager.Instance.SetText(name);
        }
		public void OnSubmit(BaseEventData eventData)
		{
			action.Execute();
		}
	}
}