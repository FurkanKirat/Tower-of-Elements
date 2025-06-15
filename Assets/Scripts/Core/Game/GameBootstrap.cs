using Core.Database;
using UnityEngine;

namespace Core.Game
{
    public class GameBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
            TileDatabase.Load();
            TowerDataDatabase.Load();
            TowerAssetDatabase.Load();
        }
    }
}