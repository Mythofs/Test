using System.IO;
using UnityEngine;

namespace Scripts.Saving
{
    public class SaveManager: MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public static SaveManager Instance;
        private readonly string savePath = Application.persistentDataPath + "/save.save";
        private void Start()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        // Update is called once per frame
        public void Save(SaveData saveData)
        {
            File.WriteAllText(savePath, JsonUtility.ToJson(saveData, true));
        }
        public SaveData Load()
        {
            string content = File.ReadAllText(savePath);
            return JsonUtility.FromJson<SaveData>(content);
        }
    }
}
