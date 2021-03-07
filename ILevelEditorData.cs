using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public interface ILevelEditorData : ILevelData
  {
    bool IsEditingEnable { get; set; }
    TileType SelectedTile { get; set; }
  }
}
