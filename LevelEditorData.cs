using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ChaseAndRun
{
  public class LevelEditorData : ILevelEditorData
  {
    // IGrid Interface
    public Vector2Int GridDimension { get { return gridDimension; } set { gridDimension = value; } }
    public Cell[,] Cells { get; set; }

    //ITile interface
    public TileType[,] tileType { get; set; }

    //Do not include below data to Json (Editor Use Only)
    [JsonIgnore]
    //ILevelEditorData interface
    public bool IsEditingEnable { get; set; } 
    [JsonIgnore]
    public TileType SelectedTile { get; set; }
    
    [SerializeField]
    private Vector2Int gridDimension;
  }
}
