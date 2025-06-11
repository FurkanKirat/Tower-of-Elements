using Core.Database;
using UnityEngine;

namespace Core.Game
{
    public class GameBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            TileDatabase.Load();
        }
    }
}