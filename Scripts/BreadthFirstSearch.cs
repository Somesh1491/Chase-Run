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

  public class BreadthFirstSearch : ShortestPathAlgorithm
  {
    private bool overlappingEnable = true;

    public override List<Vector2Int> ShortestPath { get { return GetShortestPath(); } }

    public BreadthFirstSearch(IGrid grid)
    {
      this.grid = grid;
    }

    protected override List<Vector2Int> GetShortestPath()
    {
      Node<Cell> exploringNode = CreateGraphAndReturnLastNode();
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

    private Node<Cell> CreateGraphAndReturnLastNode()
    {
      //Source and target must lie within surface
      if (!IsCellExist(SourceIndex) && !IsCellExist(TargetIndex))
        return null;

      Cell endCell = grid.Cells[TargetIndex.x, TargetIndex.y];

      Node<Cell> sourceNode = new Node<Cell>
      {
        parent = null
      };

      Queue queue = new Queue();
      queue.Enqueue(sourceNode);

      bool loopTerminationFlag = false;
      Node<Cell> lastNode = null;

      do
      {
        Node<Cell> currentNode = (Node<Cell>)queue.Dequeue();

        //Get Neighbours of current Block
        foreach (var neighbour in GetNeighbours(currentNode.item))
        {
          //if node already exist in graph 
          if (nodesInGraph.ContainsKey(GridToLinearPoint(neighbour.index)))
            continue;

          //Create new Node for neighbour Block
          Node<Cell> newNode = new Node<Cell>
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

    private List<Cell> GetNeighbours(Cell cell)
    {
      List<Cell> neighbours = new List<Cell>();

      Vector2Int leftBlockIndex = cell.index + Vector2Int.left;
      Vector2Int rightBlockIndex = cell.index + Vector2Int.right;
      Vector2Int downBlockIndex = cell.index + Vector2Int.down;
      Vector2Int upBlockIndex = cell.index + Vector2Int.up;

      Vector2Int leftUpBlockIndex = leftBlockIndex + Vector2Int.up;
      Vector2Int rightUpBlockIndex = rightBlockIndex + Vector2Int.up;
      Vector2Int leftDownBlockIndex = leftBlockIndex + Vector2Int.down;
      Vector2Int rightDownBlockIndex = rightBlockIndex + Vector2Int.down;

      if (IsCellWalkable(leftBlockIndex))
        neighbours.Add(grid.Cells[leftBlockIndex.x, leftBlockIndex.y]);

      if (IsCellWalkable(rightBlockIndex))
        neighbours.Add(grid.Cells[rightBlockIndex.x, rightBlockIndex.y]);

      if (IsCellWalkable(downBlockIndex))
        neighbours.Add(grid.Cells[downBlockIndex.x, downBlockIndex.y]);

      if (IsCellWalkable(upBlockIndex))
        neighbours.Add(grid.Cells[upBlockIndex.x, upBlockIndex.y]);

      //Diagonal Neighbour only be considered only if it neighbours are free
      if (overlappingEnable)
      {
        if (IsCellWalkable(leftUpBlockIndex))
        {
          if (IsCellWalkable(leftBlockIndex) && IsCellWalkable(upBlockIndex))
            neighbours.Add(grid.Cells[leftUpBlockIndex.x, leftUpBlockIndex.y]);
        }

        if (IsCellWalkable(rightUpBlockIndex))
        {
          if (IsCellWalkable(rightBlockIndex) && IsCellWalkable(upBlockIndex))
            neighbours.Add(grid.Cells[rightUpBlockIndex.x, rightUpBlockIndex.y]);
        }

        if (IsCellWalkable(leftDownBlockIndex))
        {
          if (IsCellWalkable(leftBlockIndex) && IsCellWalkable(downBlockIndex))
            neighbours.Add(grid.Cells[leftDownBlockIndex.x, leftDownBlockIndex.y]);
        }

        if (IsCellWalkable(rightDownBlockIndex))
        {
          if (IsCellWalkable(rightBlockIndex) && IsCellWalkable(downBlockIndex))
            neighbours.Add(grid.Cells[rightDownBlockIndex.x, rightDownBlockIndex.y]);
        }
      }

      else
      {
        if (IsCellWalkable(leftUpBlockIndex))
          neighbours.Add(grid.Cells[leftUpBlockIndex.x, leftUpBlockIndex.y]);

        if (IsCellWalkable(rightUpBlockIndex))
          neighbours.Add(grid.Cells[rightUpBlockIndex.x, rightUpBlockIndex.y]);

        if (IsCellWalkable(leftDownBlockIndex))
          neighbours.Add(grid.Cells[leftDownBlockIndex.x, leftDownBlockIndex.y]);

        if (IsCellWalkable(rightDownBlockIndex))
          neighbours.Add(grid.Cells[rightDownBlockIndex.x, rightDownBlockIndex.y]);
      }

      return neighbours;
    }

    private bool IsCellWalkable(Vector2Int index)
    {
      if (!IsCellExist(index))
        return false;

      if (grid.Cells[index.x, index.y].isBlocked)
        return false;

      else
        return true;
    }

    private bool IsCellExist(Vector2Int index)
    {
      //Horizontal Check
      if (index.x < 0 || index.x >= grid.GridDimension.x)
        return false;
      //Vertical Check
      if (index.y < 0 || index.y >= grid.GridDimension.y)
        return false;

      return true;
    }
    private int GridToLinearPoint(Vector2Int index)
    {
      return ((index.y * grid.GridDimension.x) + index.x);
    }
  }
}
