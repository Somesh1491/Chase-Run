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

    [SerializeField]
    Vector2Int gridDimension;
    [SerializeField]
    private Vector3 sourcePosition;
    [SerializeField]
    private Vector3 targetPosition;
    private Mesh mesh;
    private Block[,] blocks;
    private Dictionary<int, Node<Block>> nodesInGraph = new Dictionary<int, Node<Block>>();
    
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
            isWalkable = false
          };
        }
      }
    }

    private void Update()
    {
      DrawGrid();
    }

    private void CreateGraph()
    {
      Queue queue = new Queue();
    }

    private List<Block> GetNeighbours(Block block)
    {
      List<Block> neighbours = new List<Block>();
      
      Vector2Int leftBlockIndex = block.indexInGrid - Vector2Int.right;
      Vector2Int rightBlockIndex = block.indexInGrid + Vector2Int.right;
      Vector2Int downBlockIndex = block.indexInGrid - Vector2Int.up;
      Vector2Int upBlockIndex = block.indexInGrid + Vector2Int.down;

      Vector2Int leftUpBlockIndex = leftBlockIndex + Vector2Int.up;
      Vector2Int rightUpBlockIndex = rightBlockIndex + Vector2Int.up;
      Vector2Int leftDownBlockIndex = leftBlockIndex - Vector2Int.up;
      Vector2Int rightDownBlockIndex = rightBlockIndex - Vector2Int.up;

      Block currentBlock;

      if ((currentBlock = GetBlock(leftBlockIndex)) != null)
        neighbours.Add(currentBlock);

      if ((currentBlock = GetBlock(rightBlockIndex)) != null)
        neighbours.Add(currentBlock);

      if ((currentBlock = GetBlock(downBlockIndex)) != null)
        neighbours.Add(currentBlock);

      if ((currentBlock = GetBlock(upBlockIndex)) != null)
        neighbours.Add(currentBlock);

      if ((currentBlock = GetBlock(leftUpBlockIndex)) != null)
        neighbours.Add(currentBlock);

      if ((currentBlock = GetBlock(rightUpBlockIndex)) != null)
        neighbours.Add(currentBlock);

      if ((currentBlock = GetBlock(leftDownBlockIndex)) != null)
        neighbours.Add(currentBlock);

      if ((currentBlock = GetBlock(rightDownBlockIndex)) != null)
        neighbours.Add(currentBlock);

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

    private Vector2Int WorldToGridPoint(Vector3 worldPoint)
    {
      //Transform Matrix
      Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;

      //Origin is consider to be bottom left of the mesh
      Vector3 originInLocal = mesh.bounds.center + (Vector3.left * mesh.bounds.extents.x) +
                                                   (Vector3.down * mesh.bounds.extents.y);

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

