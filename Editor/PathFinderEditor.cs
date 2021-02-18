using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ChaseAndRun
{
  [CustomEditor(typeof(PathFinder))]
  public class PathFinderEditor : Editor
  {
    private PathFinder pathFinder;
    private Mesh mesh;
    private void OnEnable()
    {
      pathFinder = (PathFinder)target;
      mesh = pathFinder.GetComponent<MeshFilter>().sharedMesh;
    }

    private void OnSceneGUI()
    {
      
    }

    private void DrawGrid()
    {

    }
  }
}
