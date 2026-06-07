using UnityEngine;

namespace Scripts.Interactables
{
	[System.Serializable]
	public class InteractableData
	{
		public string id;
		public string data;
		public InteractableData(string id, string data)
		{
			this.id = id;
			this.data = data;
		}
	}
}