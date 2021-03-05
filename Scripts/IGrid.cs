using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public struct Cell<T>
  {
    public T item;
    public Vector2Int index;
    public int weight;
  }
  public interface IGrid<T>
  {
    Vector2Int GridDimension { get; set; }
    Cell<T>[] Cells { get; set; }

    bool IsCellExist(Vector2Int index);
    Cell<T> GetCell(Vector2Int index);
    void SetCell(Vector2Int index, T value);
  }
}
