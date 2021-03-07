using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public abstract class ShortestPathAlgorithm
  {
    public Vector2Int SourceIndex { get; set; }
    public Vector2Int TargetIndex { get; set; }

    protected IGrid grid;

    protected Dictionary<int, Node<Cell>> nodesInGraph = new Dictionary<int, Node<Cell>>();

    public abstract List<Vector2Int> ShortestPath { get; }

    protected abstract List<Vector2Int> GetShortestPath();
  }
}
