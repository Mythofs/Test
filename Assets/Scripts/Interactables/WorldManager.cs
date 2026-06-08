using Scripts.Managers;
using System.Linq;
using UnityEngine;

namespace Scripts.Interactables
{
	public class WorldManager : MonoBehaviour
	{
		public static WorldManager Instance;
		ISaveable[] saveableObjects;
		private void Awake()
		{
			if (Instance == null)
				Instance = this;
			else
				Destroy(this);
		}
		private void Start()
		{
            saveableObjects = FindObjectsByType<MonoBehaviour>().OfType<ISaveable>().ToArray();
        }
        public void Save()
		{
			foreach (ISaveable saveable in saveableObjects)
				GameManager.Instance.saveData.worldData.interactableList.Add(new InteractableData(saveable.Id, saveable.Serialize()));
		}
		public void Load(WorldData worldData)
		{
			foreach (InteractableData data in worldData.interactableList)
				foreach (ISaveable saveable in saveableObjects)
					if (data.id == saveable.Id)
						saveable.Deserialize(data.data);
		}
	}
}