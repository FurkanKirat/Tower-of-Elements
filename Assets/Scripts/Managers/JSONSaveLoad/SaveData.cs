using System;
using System.Collections.Generic;

namespace Managers
{
    [Serializable]
    public class SaveData
    {
        public int totalStars;
        public int totalGems;
        public List<LevelData> levelData = new List<LevelData>();
        public List<Characters> characterData = new List<Characters>();
        public List<Inventory> iventoryData = new List<Inventory>();
    }
    
    [Serializable]
    public class LevelData
    {
        public int starsEarned;
        public bool isUnlocked; 
    }

    [Serializable]
    public class Characters
    {
        public bool isUnlocked;
        public string name;
        public int characterLevel;
        
    }
    [Serializable]
    public class Inventory
    {
        public string name;
        public int quantity;
    }
        
}