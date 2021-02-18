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
    
    private void Awake()
    {
      if((mesh=GetComponent<MeshFilter>().sharedMesh) == null)
      {
        Debug.LogError("No Mesh Attached in GameObject");
        return;
      }
    }

    private void Start()
    {
      
    }

    private void CreateGraph()
    {

    }
  }
}

