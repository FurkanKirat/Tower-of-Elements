using Core.Constants;
using Managers;

namespace Core.Map
{
    public static class MapLoader
    {
        public static MapData LoadMap(string name)
        {
            string path = PathHelper.CombineWithResources(PathNames.RootFolders.Maps, name + PathNames.Extensions.Json);
            return JsonHelper.Load<MapData>(path);
        }
    }
}