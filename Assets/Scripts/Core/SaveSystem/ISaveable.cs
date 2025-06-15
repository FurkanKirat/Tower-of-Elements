namespace Core.SaveSystem
{
    public interface ISaveable<TSave>
    {
        TSave ToSaveData();
        void LoadFromSaveData(TSave data);
    }
}