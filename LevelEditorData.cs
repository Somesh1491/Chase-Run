using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class LevelEditorData : ILevelEditorData
  {
    // IGrid Interface
    public Vector2Int GridDimension { get { return gridDimension; } set { gridDimension = value; } }
    public Cell[,] Cells { get; set; }

    //ITile interface
    public TileType[,] tileType { get; set; }

    //ILevelEditorData interface
    public bool IsEditingEnable { get; set; }    
    public TileType SelectedTile { get; set; }
    
    [SerializeField]
    private Vector2Int gridDimension;

    public Cell GetCell(Vector2Int index)
    {
      if(IsCellExist(index))
        return Cells[index.x, index.y];

      return default;
    }

    public void SetCell(Vector2Int index, Cell value)
    {
      Cells[index.x, index.y] = value;
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
