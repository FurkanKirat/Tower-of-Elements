using Core.Constants;
using Managers;

namespace Core.Map
{
    public static class MapLoader
    {
        public static MapData LoadMap(string mapId)
        {
            string path = PathHelper.CombineWithResources(PathNames.RootFolders.Maps, mapId + PathNames.Extensions.Json);
            var mapData = JsonHelper.Load<MapData>(path);
            mapData.UpdateGrid();
            return mapData;
        }
    }
}