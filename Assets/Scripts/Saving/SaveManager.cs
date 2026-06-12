using System.IO;
using UnityEngine;

namespace Scripts.Saving
{
    public class SaveManager: MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public static SaveManager Instance;
        private string savePath;
        private void Awake()
        {
            savePath = Application.persistentDataPath + "/save.save";
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
            Debug.Log(savePath);
            if (File.Exists(savePath))
            {
                string content = File.ReadAllText(savePath);
                return JsonUtility.FromJson<SaveData>(content);
            }
            return null;
        }
    }
}
