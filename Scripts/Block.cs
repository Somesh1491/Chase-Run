using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class Block : MonoBehaviour, IBlock
  {
    public Vector2Int IndexInGrid { get { return indexInGrid; } set { indexInGrid = value; } }
    public bool IsWalkable { get { return isWalkable; } set { isWalkable = value; } }

    private Vector2Int indexInGrid;
    private bool isWalkable;
  }
}
