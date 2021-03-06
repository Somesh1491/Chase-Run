using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class LevelData : ScriptableObject, ILevelData
  {
    public bool IsEditingEnable { get; set; }
    public Vector2Int GridDimension { get { return gridDimension; } set { gridDimension = value; } }
    public Cell<TileType>[] Cells { get; set; }
    public TileType SelectedTile { get; set; }

    [SerializeField]
    private Vector2Int gridDimension;

    public Cell<TileType> GetCell(Vector2Int index)
    {
      if(IsCellExist(index))
        return Cells[gridDimension.x * index.y + index.x];

      return default;
    }

    public void SetCell(Vector2Int index, TileType value)
    {
      Cells[gridDimension.x * index.y + index.x].item = value;

      Cells[gridDimension.x * index.y + index.x].index = index;

      switch (value)
      {
        case TileType.Walkable:
          Cells[gridDimension.x * index.y + index.x].weight = 1;
          break;

        case TileType.Obstacle:
          Cells[gridDimension.x * index.y + index.x].weight = -1;
          break;

        default:
          break;
      }
    }

    public bool IsCellExist(Vector2Int index)
    {
      //Horizontal Check
      if (index.x < 0 || index.x >= GridDimension.x)
        return false;
      //Vertical Check
      if (index.y < 0 || index.y >= GridDimension.y)
        return false;

      return true;
    }
  }
}
