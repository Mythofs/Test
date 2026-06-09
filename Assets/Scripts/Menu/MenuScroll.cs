using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Scripts.Menu
{
	public class MenuScroll: MonoBehaviour
	{
		[SerializeField] ScrollRect scroll;
		private RectTransform[] slots;
		private RectTransform selected;
		private void Awake()
		{
			slots = scroll.content.GetComponents<RectTransform>();
		}
		private void Update()
		{

		}
	}
}