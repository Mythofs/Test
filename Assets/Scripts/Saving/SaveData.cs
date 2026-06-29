using Scripts.Interactables;
using Scripts.Inventory;
using Scripts.Player;

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