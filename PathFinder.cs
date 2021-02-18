using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class PathFinder : MonoBehaviour
  {
    public Vector2Int GridDimension { get { return gridDimension; } }
 
    [SerializeField]
    Vector2Int gridDimension;
  }
}

