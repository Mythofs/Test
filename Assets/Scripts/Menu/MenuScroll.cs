using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Scripts.Menu
{
	public class MenuScroll: MonoBehaviour
	{
		[SerializeField] ScrollRect scroll;
		private RectTransform[] slots;
		private GameObject lastSelected = null;
		private void Awake()
		{
			slots = scroll.content.GetComponentsInChildren<RectTransform>();
		}
		private void Update()
		{
			GameObject current = EventSystem.current.currentSelectedGameObject;
			if(current != null && current != lastSelected && current.transform.IsChildOf(scroll.content))
			{
				UpdateMenu(current);
				lastSelected = current;
			}
		}
		private void UpdateMenu(GameObject current)
		{
			RectTransform currentTransform = current.GetComponent<RectTransform>();
			Vector3 distance = transform.position - currentTransform.position;
			foreach(RectTransform slot in slots)
				slot.position += distance;
		}
	}
}