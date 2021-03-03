using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class LevelData : ILevelData
  {
    public Vector2Int GridDimension { get { return gridDimension; } set { gridDimension = value; } }

    private Vector2Int gridDimension;
  }
}
