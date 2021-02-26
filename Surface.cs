using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class Surface : MonoBehaviour
  {
    public Vector2Int GridDimension { get { return gridDimension; } }
    public Mesh Mesh { get { return GetComponent<MeshFilter>().sharedMesh; } }

    [SerializeField]
    private Vector2Int gridDimension;

  }
}
