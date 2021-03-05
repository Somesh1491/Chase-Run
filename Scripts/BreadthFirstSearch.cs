using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class Node<T>
  {
    public T item;
    public Node<T> parent;
  }

  public class BreadthFirstSearch<T> : ShortestPathAlgorithm<T>
  {
    private bool overlappingEnable = true;

    public override List<Vector2Int> ShortestPath { get { return GetShortestPath(); } }

    public BreadthFirstSearch(IGrid<T> grid)
    {
      this.grid = grid;
    }

    protected override List<Vector2Int> GetShortestPath()
    {
      Node<Cell<T>> exploringNode = CreateGraphAndReturnLastNode();
      List<Vector2Int> shortestPath = new List<Vector2Int>();

      //Do not include the start node
      if (exploringNode != null)
      {
        while (exploringNode.parent != null)
        {
          shortestPath.Add(exploringNode.item.index);
          exploringNode = exploringNode.parent;
        }
      }

      shortestPath.Reverse();
      return shortestPath;
    }

    private Node<Cell<T>> CreateGraphAndReturnLastNode()
    {
      //Source and target must lie within surface
      if (!grid.IsCellExist(SourceIndex) && !grid.IsCellExist(TargetIndex))
        return null;

      Cell<T> startCell = grid.GetCell(SourceIndex);
      Cell<T> endCell = grid.GetCell(TargetIndex);

      Node<Cell<T>> sourceNode = new Node<Cell<T>>
      {
        parent = null
      };

      Queue queue = new Queue();
      queue.Enqueue(sourceNode);

      bool loopTerminationFlag = false;
      Node<Cell<T>> currentNode = null;
      Node<Cell<T>> lastNode = null;

      do
      {
        currentNode = (Node<Cell<T>>)queue.Dequeue();

        //Get Neighbours of current Block
        foreach (var neighbour in GetNeighbours(currentNode.item))
        {
          //if node already exist in graph 
          if (nodesInGraph.ContainsKey(GridToLinearPoint(neighbour.index)))
            continue;

          //Create new Node for neighbour Block
          Node<Cell<T>> newNode = new Node<Cell<T>>
          {
            parent = currentNode
          };

          nodesInGraph.Add(GridToLinearPoint(neighbour.index), newNode);
          queue.Enqueue(newNode);

          if (neighbour.Equals(endCell))
          {
            loopTerminationFlag = true;
            lastNode = newNode;
            break;
          }
        }

      } while (queue.Count != 0 && !loopTerminationFlag);

      return lastNode;
    }

    private List<Cell<T>> GetNeighbours(Cell<T> cell)
    {
      List<Cell<T>> neighbours = new List<Cell<T>>();

      Vector2Int leftBlockIndex = cell.index + Vector2Int.left;
      Vector2Int rightBlockIndex = cell.index + Vector2Int.right;
      Vector2Int downBlockIndex = cell.index + Vector2Int.down;
      Vector2Int upBlockIndex = cell.index + Vector2Int.up;

      Vector2Int leftUpBlockIndex = leftBlockIndex + Vector2Int.up;
      Vector2Int rightUpBlockIndex = rightBlockIndex + Vector2Int.up;
      Vector2Int leftDownBlockIndex = leftBlockIndex + Vector2Int.down;
      Vector2Int rightDownBlockIndex = rightBlockIndex + Vector2Int.down;

      if (IsCellWalkable(leftBlockIndex))
        neighbours.Add(grid.GetCell(leftBlockIndex));

      if (IsCellWalkable(rightBlockIndex))
        neighbours.Add(grid.GetCell(rightBlockIndex));

      if (IsCellWalkable(downBlockIndex))
        neighbours.Add(grid.GetCell(downBlockIndex));

      if (IsCellWalkable(upBlockIndex))
        neighbours.Add(grid.GetCell(upBlockIndex));

      //Diagonal Neighbour only be considered only if it neighbours are free
      if (overlappingEnable)
      {
        if (IsCellWalkable(leftUpBlockIndex))
        {
          if (IsCellWalkable(leftBlockIndex) && IsCellWalkable(upBlockIndex))
            neighbours.Add(grid.GetCell(leftUpBlockIndex));
        }

        if (IsCellWalkable(rightUpBlockIndex))
        {
          if (IsCellWalkable(rightBlockIndex) && IsCellWalkable(upBlockIndex))
            neighbours.Add(grid.GetCell(rightUpBlockIndex));
        }

        if (IsCellWalkable(leftDownBlockIndex))
        {
          if (IsCellWalkable(leftBlockIndex) && IsCellWalkable(downBlockIndex))
            neighbours.Add(grid.GetCell(leftDownBlockIndex));
        }

        if (IsCellWalkable(rightDownBlockIndex))
        {
          if (IsCellWalkable(rightBlockIndex) && IsCellWalkable(downBlockIndex))
            neighbours.Add(grid.GetCell(rightDownBlockIndex));
        }
      }

      else
      {
        if (IsCellWalkable(leftUpBlockIndex))
          neighbours.Add(grid.GetCell(leftUpBlockIndex));

        if (IsCellWalkable(rightUpBlockIndex))
          neighbours.Add(grid.GetCell(rightUpBlockIndex));

        if (IsCellWalkable(leftDownBlockIndex))
          neighbours.Add(grid.GetCell(leftDownBlockIndex));

        if (IsCellWalkable(rightDownBlockIndex))
          neighbours.Add(grid.GetCell(rightDownBlockIndex));
      }

      return neighbours;
    }

    private bool IsCellWalkable(Vector2Int index)
    {
      if (!grid.IsCellExist(index))
        return false;

      if (grid.GetCell(index).weight == -1)
        return false;

      else
        return true;
    }

    private int GridToLinearPoint(Vector2Int index)
    {
      return ((index.y * grid.GridDimension.x) + index.x);
    }
  }
}
