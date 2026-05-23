using UnityEngine;

namespace Scripts.Portals
{
	public class Portal : MonoBehaviour
	{
		[SerializeField] private Transform destination;
		private Vector2 offset = new Vector2(0, 0.3f);
		public void Warp(Transform player)
		{
			player.position = new Vector2(destination.position.x + offset.x, destination.position.y + offset.y);
		}
	}
}