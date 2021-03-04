using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class LevelData : ILevelData
  {
    public Vector2Int GridDimension { get { return gridDimension; } set { gridDimension = value; } }

    public bool IsEditingEnable { get { return isEditingEnable; } set { isEditingEnable = value; } }

    public TileType[,] Tiles { get { return tiles; } set { tiles = value; } }

    public TileType SelectedTile { get { return selectedTile; } set { selectedTile = value; } }

    private Vector2Int gridDimension;
    private bool isEditingEnable;
    private TileType[,] tiles;
    private TileType selectedTile;
  }
}
