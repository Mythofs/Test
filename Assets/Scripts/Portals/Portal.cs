using UnityEngine;

namespace Scripts.Portals
{
	public class Portal : MonoBehaviour
	{
		[SerializeField] private Transform destination;
		public void Warp(Collider2D player)
		{
			player.transform.position = destination.position;
		}
	}
}