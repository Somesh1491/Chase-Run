using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class LevelData : ScriptableObject, ILevelData
  {
    public bool IsEditingEnable { get; set; }
    public Vector2Int GridDimension { get { return gridDimension; } set { gridDimension = value; } }
    public Cell<TileType>[] Cells { get { return cells; } set { cells = value; } }
    public TileType SelectedTile { get; set; }

    [SerializeField]
    private Vector2Int gridDimension;
    [SerializeField]
    private Cell<TileType>[] cells;


    public void CreateTiles(Vector2Int dimension)
    {
      Cells = new Cell<TileType>[dimension.x * dimension.y];
    }

    public Cell<TileType> GetCell(Vector2Int index)
    {
      return Cells[gridDimension.x * index.y + index.x];
    }

    public void SetCell(Vector2Int index, TileType value)
    {
      Cells[gridDimension.x * index.y + index.x].item = value;
    }
  }
}
