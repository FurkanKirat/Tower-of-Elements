using System;
using System.Collections.Generic;
using System.Linq;
using Core.Constants;
using Core.Entities;
using Core.GridSystem;
using Core.Math;
using Newtonsoft.Json;

namespace Core.Map
{
    [Serializable]
    public class MapData
    {
        public string mapId;
        public Vec2Int enemySpawn;
        public List<GridCell> cells;
        public List<EnemyData> enemies;
        
        [JsonIgnore]
        public GridCell[,] Grid;

        public void UpdateGrid()
        {
            Grid = new GridCell[GridConstants.GridWidth, GridConstants.GridHeight];
            foreach (var cell in cells)
            {
                Grid[cell.Position.x, cell.Position.y] = cell;
            }
        }

        public void UpdateCellList()
        {
            cells.Clear();
            foreach (var cell in Grid)
                cells.Add(cell);
        }
        public GridCell GetCellFromList(Vec2Int cellPos)
        {
            return cells.FirstOrDefault(cell => cell.Position == cellPos);
        }

        public GridCell GetCellFromGrid(Vec2Int cellPos)
        {
            return Grid[cellPos.x, cellPos.y];
        }
    }
}