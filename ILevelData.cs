using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public interface ILevelData : IGrid<TileType>
  {
    TileType SelectedTile { get; set; }
    bool IsEditingEnable { get; set; }
  }
}
