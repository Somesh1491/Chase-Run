using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public struct Cell
  {
    public bool isBlocked;
    public Vector2Int index;
  }
  public interface IGrid
  {
    Vector2Int GridDimension { get; set; }
    Cell[,] Cells { get; set; }
  }
}
