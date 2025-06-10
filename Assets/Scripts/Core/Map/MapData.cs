using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.GridSystem;
using Core.Math;

namespace Core.Map
{
    [Serializable]
    public class MapData
    {
        public string mapId;
        public Vec2Int enemySpawn;
        public List<GridCell> cells;
        public List<EnemyData> enemies;

        public GridCell GetCell(Vec2Int cellPos)
        {
            return cells.FirstOrDefault(cell => cell.Position == cellPos);
        }
    }
}