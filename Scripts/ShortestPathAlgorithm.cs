using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public abstract class ShortestPathAlgorithm<T>
  {
    public Vector2Int SourceIndex { get; set; }
    public Vector2Int TargetIndex { get; set; }

    protected IGrid<T> grid;

    protected Dictionary<int, Node<Cell<T>>> nodesInGraph = new Dictionary<int, Node<Cell<T>>>();

    public abstract List<Vector2Int> ShortestPath { get; }

    protected abstract List<Vector2Int> GetShortestPath();
  }
}
