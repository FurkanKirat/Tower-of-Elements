using System.Collections.Generic;
using Managers;

namespace Core.SaveSystem
{
    public static class SaveManager
    {
        public static void Save<TSave>(IEnumerable<ISaveable<TSave>> saveables, string path)
        {
            var saveList = new List<TSave>();
            foreach (var s in saveables)
                saveList.Add(s.ToSaveData());

            JsonHelper.Save(path, saveList);
        }
        
        public static void Load<TSave, TObject>(string path, List<TObject> output)
            where TObject : ISaveable<TSave>, new()
        {
            var saveList = JsonHelper.Load<List<TSave>>(path);

            foreach (var save in saveList)
            {
                var obj = new TObject();
                obj.LoadFromSaveData(save);
                output.Add(obj);
            }
        }
    }


}