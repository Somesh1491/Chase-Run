﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public interface ILevelData
  {
    Vector2Int GridDimension { get; set; }
    TileType[,] Tiles { get; set; }
    TileType SelectedTile { get; set; }
    bool IsEditingEnable { get; set; }
  }
}
