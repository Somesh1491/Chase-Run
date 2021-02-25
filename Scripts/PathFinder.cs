using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class PathFinder : MonoBehaviour
  {
    public Vector2Int GridDimension { get { return gridDimension; } }
    public Vector3 SourcePosition { get { return sourcePosition; } set { sourcePosition = value; } }
    public Vector3 TargetPosition { get { return targetPosition; } set { targetPosition = value; } }
    public List<Vector3> ShortestPath { get { return GetShortestPath(); } }

    [SerializeField]
    Vector2Int gridDimension;
    [SerializeField]
    private Vector3 sourcePosition;
    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private bool overlappingEnable = true; 
    private Mesh mesh;
    private Block[,] blocks;
    private Dictionary<int, Node<Block>> nodesInGraph = new Dictionary<int, Node<Block>>();
    //Deleteing Block
    [SerializeField]
    GameObject nodeVisitedPrefab;
    [SerializeField]
    GameObject shortPathPrefab;

    List<GameObject> tempObject = new List<GameObject>();
    //Deleting Block

    private void Awake()
    {
      if((mesh=GetComponent<MeshFilter>().sharedMesh) == null)
      {
        Debug.LogError("No Mesh Attached in GameObject");
        return;
      }

      blocks = new Block[gridDimension.x, gridDimension.y];
    }

    private void Start()
    {
      for(int j = 0; j < blocks.GetLength(1); j++)
      {
        for(int i = 0; i < blocks.GetLength(0); i++)
        {
          blocks[i, j] = new Block
          {
            indexInGrid = new Vector2Int(i, j),
            isWalkable = true
          };
        }
      }

      int childCount = transform.childCount;
      for(int i = 0; i < childCount; i++)
      {
        Tile tile = transform.GetChild(i).GetComponent<Tile>();
        if (tile == null)
          continue;

        if(tile.Type == TileType.Obstacle)
        {
          Vector2Int index = WorldToGridPoint(tile.transform.position);
          blocks[index.x, index.y].isWalkable = false;
        }
      }
    }

    private void Update()
    {
      DrawGrid();

      Vector2Int gridPos = WorldToGridPoint(sourcePosition);
    }

    private List<Vector3> GetShortestPath()
    {
      /************Testing Code Block*****************/
      foreach (var obj in tempObject)
        Destroy(obj);
      /************Testing Code Block*****************/

      List<Vector3> shortestPath = new List<Vector3>();
      Node<Block> exploringNode = CreateGraphAndReturnLastNode();

      //Do not include the start node
      if (exploringNode != null)
      {
        while (exploringNode.parent != null)
        {
          shortestPath.Add(GridToWorldPoint(exploringNode.item.indexInGrid));
          exploringNode = exploringNode.parent;
        }
      }
      /************Testing Code Block*****************/
      foreach (KeyValuePair<int, Node<Block>>node in nodesInGraph)
      {
        //Size of each block In Local Space
        float blockWidth = (mesh.bounds.size.x * transform.localScale.x) / gridDimension.x;
        float blockHeight = (mesh.bounds.size.y * transform.localScale.y) / gridDimension.y;

        GameObject g = Instantiate(nodeVisitedPrefab);
        g.transform.position = GridToWorldPoint(node.Value.item.indexInGrid);
        g.transform.localScale = new Vector3(blockWidth, blockHeight);
        tempObject.Add(g);
      }

      foreach(var position in shortestPath)
      {
        //Size of each block In Local Space
        float blockWidth = (mesh.bounds.size.x * transform.localScale.x) / gridDimension.x;
        float blockHeight = (mesh.bounds.size.y * transform.localScale.y) / gridDimension.y;

        GameObject g = Instantiate(shortPathPrefab);
        g.transform.position = position;
        g.transform.localScale = new Vector3(blockWidth, blockHeight);
        tempObject.Add(g);
      }
      /************Testing Code Block*****************/

      shortestPath.Reverse();
      return shortestPath;
    }

    private Node<Block> CreateGraphAndReturnLastNode()
    {
      //Clear nodes In Graph
      nodesInGraph.Clear();

      Vector2Int startBlockIndex = WorldToGridPoint(sourcePosition);
      Vector2Int endBlockIndex = WorldToGridPoint(targetPosition);

      //Source and target must lie within surface
      if (!IsBlockExist(startBlockIndex) && !IsBlockExist(endBlockIndex))
        return null;

      Block startBlock = GetBlock(startBlockIndex);
      Block endBlock = GetBlock(endBlockIndex);

      Node<Block> startNode = new Node<Block>
      {
        item = startBlock,
        parent = null
      };

      Queue queue = new Queue();
      queue.Enqueue(startNode);

      bool loopTerminationFlag = false;
      Node<Block> currentNode = null;
      Node<Block> lastNode = null;
      do
      {
        currentNode = (Node<Block>)queue.Dequeue();

        //Get Neighbours of current Block
        foreach(var neighbour in GetNeighbours(currentNode.item))
        {
          //if node already exist in graph 
          if (nodesInGraph.ContainsKey(GridToLinearPoint(neighbour.indexInGrid)))
            continue;

          //Create new Node for neighbour Block
          Node<Block> newNode = new Node<Block>
          {
            item = neighbour,
            parent = currentNode
          };

          nodesInGraph.Add(GridToLinearPoint(neighbour.indexInGrid), newNode);
          queue.Enqueue(newNode);

          if(neighbour == endBlock)
          {
            loopTerminationFlag = true;
            lastNode = newNode;
            break;
          }
        }

      } while (queue.Count != 0 && !loopTerminationFlag);

      return lastNode;
    }

    private List<Block> GetNeighbours(Block block)
    {
      List<Block> neighbours = new List<Block>();
      
      Vector2Int leftBlockIndex = block.indexInGrid + Vector2Int.left;
      Vector2Int rightBlockIndex = block.indexInGrid + Vector2Int.right;
      Vector2Int downBlockIndex = block.indexInGrid + Vector2Int.down;
      Vector2Int upBlockIndex = block.indexInGrid + Vector2Int.up;

      Vector2Int leftUpBlockIndex = leftBlockIndex + Vector2Int.up;
      Vector2Int rightUpBlockIndex = rightBlockIndex + Vector2Int.up;
      Vector2Int leftDownBlockIndex = leftBlockIndex + Vector2Int.down;
      Vector2Int rightDownBlockIndex = rightBlockIndex + Vector2Int.down;

      if (IsBlockWalkable(leftBlockIndex))
          neighbours.Add(GetBlock(leftBlockIndex));

      if (IsBlockWalkable(rightBlockIndex))
        neighbours.Add(GetBlock(rightBlockIndex));

      if (IsBlockWalkable(downBlockIndex))
        neighbours.Add(GetBlock(downBlockIndex));

      if (IsBlockWalkable(upBlockIndex))
        neighbours.Add(GetBlock(upBlockIndex));

      //Diagonal Neighbour only be considered only if it neighbours are free
      if (overlappingEnable)
      {
        if (IsBlockWalkable(leftUpBlockIndex))
        {
          if(IsBlockWalkable(leftBlockIndex) && IsBlockWalkable(upBlockIndex))
            neighbours.Add(GetBlock(leftUpBlockIndex));
        }

        if (IsBlockWalkable(rightUpBlockIndex))
        {
          if (IsBlockWalkable(rightBlockIndex) && IsBlockWalkable(upBlockIndex))
            neighbours.Add(GetBlock(rightUpBlockIndex));
        }

        if (IsBlockWalkable(leftDownBlockIndex))
        {
          if (IsBlockWalkable(leftBlockIndex) && IsBlockWalkable(downBlockIndex))
            neighbours.Add(GetBlock(leftDownBlockIndex));
        }

        if (IsBlockWalkable(rightDownBlockIndex))
        {
          if (IsBlockWalkable(rightBlockIndex) && IsBlockWalkable(downBlockIndex))
            neighbours.Add(GetBlock(rightDownBlockIndex));
        }
      }

      else
      {
        if (IsBlockWalkable(leftUpBlockIndex))
          neighbours.Add(GetBlock(leftUpBlockIndex));

        if (IsBlockWalkable(rightUpBlockIndex))
          neighbours.Add(GetBlock(rightUpBlockIndex));

        if (IsBlockWalkable(leftDownBlockIndex))
          neighbours.Add(GetBlock(leftDownBlockIndex));

        if (IsBlockWalkable(rightDownBlockIndex))
          neighbours.Add(GetBlock(rightDownBlockIndex));
      }
      

      return neighbours;
    }

    private Block GetBlock(Vector2Int index)
    {
      if (IsBlockExist(index))
        return blocks[index.x, index.y];

      return null;
    }

    private bool IsBlockExist(Vector2Int index)
    {
      //Horizontal Check
      if (index.x < 0 || index.x >= blocks.GetLength(0))
        return false;
      //Vertical Check
      if (index.y < 0 || index.y >= blocks.GetLength(1))
        return false;

      return true;
    }

    private bool IsBlockWalkable(Vector2Int index)
    {
      if (!IsBlockExist(index))
        return false;

      return (blocks[index.x, index.y].isWalkable);
    }

    private Vector2Int WorldToGridPoint(Vector3 worldPoint)
    {
      //Transform Matrix
      Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;

      //Size of surface in Local
      float surfaceWidth = mesh.bounds.size.x;
      float surfaceHeight = mesh.bounds.size.y;

      float blockCountPerUnit_X = gridDimension.x / surfaceWidth;
      float blockCountPerUnit_Y = gridDimension.y / surfaceHeight;

      //Local Point w.r.t origin of the mesh (i.e. Center of Object (Default))
      Vector3 localPoint = worldToLocalMatrix.MultiplyPoint3x4(worldPoint);
      //Local Point w.r.t origin as bottom left of mesh
      localPoint += (Vector3.right * mesh.bounds.extents.x) + (Vector3.up * mesh.bounds.extents.y);

      //correct work grid points x, y > 0 (taking the left int value of decimal)
      //will shows weird behaviour with -ve value (reason ^) 
      //since -ve grid point is invalid in calculation so no effect 
      Vector2Int gridPoint = new Vector2Int((int)(localPoint.x * blockCountPerUnit_X), 
                                            (int)(localPoint.y * blockCountPerUnit_Y));
 
      return gridPoint;
    }

    private Vector3 GridToWorldPoint(Vector2Int gridIndex)
    {
      //Transform Matrix
      Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;

      //Size of each block In Local Space
      float blockWidth = mesh.bounds.size.x / gridDimension.x;
      float blockHeight = mesh.bounds.size.y / gridDimension.y;

      //position considering bottom left of Surface as origin In Local
      Vector3 positionInLocal = new Vector3(gridIndex.x * blockWidth, gridIndex.y * blockHeight, 0);
      //position considering center of block.
      positionInLocal += (Vector3.right * blockWidth / 2) + (Vector3.up * blockHeight / 2);
      //position considering center of surface as pivot In Local
      positionInLocal -= (Vector3.right * mesh.bounds.extents.x) + (Vector3.up * mesh.bounds.extents.y);

      //worldPosition
      return localToWorldMatrix.MultiplyPoint3x4(positionInLocal);
    }
    private int GridToLinearPoint(Vector2Int gridIndex)
    {
      return ((gridIndex.y * gridDimension.x) + gridIndex.x);
    }

    private Vector2Int LinearToGridPoint(int linearIndex)
    {
      int x = linearIndex % gridDimension.x;
      int y = linearIndex / gridDimension.y;

      return new Vector2Int(x, y);
    }


    /********************************************************************************************************/                                  
    /************************Below Codes is are for testing purpose (need to deleted later) 
    /********************************************************************************************************/

    private void DrawGrid()
    {
      //Transform Matrix
      Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;

      //Origin is consider to be bottom left of the mesh
      Vector3 originInLocal = mesh.bounds.center + (Vector3.left * mesh.bounds.extents.x) +
                                                   (Vector3.down * mesh.bounds.extents.y);

      //Size of each block In Local Space
      float blockWidth = mesh.bounds.size.x / GridDimension.x;
      float blockHeight = mesh.bounds.size.y / GridDimension.y;

      for (int j = 0; j < GridDimension.y; j++)
      {
        for (int i = 0; i < GridDimension.x; i++)
        {
          //Points in Local
          Vector3 leftDownPoint = originInLocal + (i * Vector3.right * blockWidth) + (j * Vector3.up * blockHeight);
          Vector3 rightDownPoint = leftDownPoint + Vector3.right * blockWidth;
          Vector3 leftUpPoint = leftDownPoint + Vector3.up * blockHeight;
          Vector3 rightUpPoint = leftDownPoint + Vector3.right * blockWidth + Vector3.up * blockHeight;

          //Points in World
          leftDownPoint = localToWorldMatrix.MultiplyPoint3x4(leftDownPoint);
          rightDownPoint = localToWorldMatrix.MultiplyPoint3x4(rightDownPoint);
          leftUpPoint = localToWorldMatrix.MultiplyPoint3x4(leftUpPoint);
          rightUpPoint = localToWorldMatrix.MultiplyPoint3x4(rightUpPoint);

          DrawQuad(leftDownPoint, rightDownPoint, leftUpPoint, rightUpPoint);
        }
      }
    }

    private void DrawQuad(Vector3 leftDownPoint, Vector3 rightDownPoint, Vector3 leftUpPoint, Vector3 rightUpPoint)
    {
      Debug.DrawLine(leftDownPoint, rightDownPoint, Color.cyan);
      Debug.DrawLine(leftDownPoint, leftUpPoint, Color.cyan);
      Debug.DrawLine(leftUpPoint, rightUpPoint, Color.cyan);
      Debug.DrawLine(rightDownPoint, rightUpPoint, Color.cyan);
    }
  }
}

