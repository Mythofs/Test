using UnityEngine;
using System.Collections;

namespace Scripts.Interactables
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class YSort: MonoBehaviour
	{
		private SpriteRenderer spriteRenderer;
		private void Awake()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		void LateUpdate()
		{
			spriteRenderer.sortingOrder = (int)(transform.position.y * -100f);
		}
	}
}