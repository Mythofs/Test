using UnityEngine;
using Scripts.Player;
using Scripts.Inventory;

namespace Scripts.Saving
{
	[System.Serializable]
	public class SaveData: MonoBehaviour
	{
		public PlayerData playerData = new();
		public InventoryData inventoryData = new();
	}
}