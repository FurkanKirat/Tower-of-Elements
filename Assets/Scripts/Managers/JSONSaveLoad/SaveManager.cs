using System.IO;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        private string savePath = Application.persistentDataPath + "/save.json";
        public SaveData currentSave = new SaveData();
        
        public void SaveGame()
        {
            string json = JsonUtility.ToJson(currentSave, true);
            File.WriteAllText(savePath, json);

        }

        public void LoadGame()
        {
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                currentSave = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                CreateDefaultSave();
                SaveGame();
            }
        }

        public void CreateDefaultSave()
        {
            currentSave.totalStars = 0;
            currentSave.totalGems = 0;
        }
    }
}