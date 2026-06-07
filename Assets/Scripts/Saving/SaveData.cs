using UnityEngine;
using Scripts.Player;
using Scripts.Inventory;
using Scripts.Interactables;
using System.Collections.Generic;

namespace Scripts.Saving
{
	[System.Serializable]
	public class SaveData
	{
		public PlayerData playerData = new();
		public InventoryData inventoryData = new();
		public WorldData worldData = new();
	}
}