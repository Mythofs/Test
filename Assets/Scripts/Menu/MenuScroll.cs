using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Scripts.Menu
{
	public class MenuScroll: MonoBehaviour
	{
		[SerializeField] RectTransform content;
		private GameObject lastSelected = null;
		private void Update()
		{
			GameObject current = EventSystem.current.currentSelectedGameObject;
			if(current != null && current != lastSelected && current.transform.IsChildOf(content))
			{
				UpdateMenu(current.GetComponent<RectTransform>());
				lastSelected = current;
			}
		}
		private void UpdateMenu(RectTransform current)
		{
			Canvas.ForceUpdateCanvases();
			content.anchoredPosition = new Vector2(-content.InverseTransformPoint(current.position).x, -256);
		}
	}
}