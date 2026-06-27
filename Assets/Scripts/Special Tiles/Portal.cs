using UnityEngine;

namespace Scripts.SpecialTiles
{
	public class Portal : MonoBehaviour, ISpecialTile
	{
		[SerializeField] private Transform destination;
		[SerializeField] private Transform player;
		private Vector2 offset = new Vector2(0, 0.3f);
		public void Interact()
		{
			player.position = new Vector2(destination.position.x + offset.x, destination.position.y + offset.y);
		}
	}
}