using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public interface IBlock
  {
    Vector2Int IndexInGrid { get; set; }
    bool IsWalkable { get; set; }
  }
}
