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
      DrawGrid();
    }

    private void DrawGrid()
    {
      //Transform Matrix
      Matrix4x4 localToWorldMatrix = pathFinder.transform.localToWorldMatrix;

      //Origin is consider to be bottom left of the mesh
      Vector3 originInLocal = mesh.bounds.center + (Vector3.left * mesh.bounds.extents.x) + 
                                                   (Vector3.down * mesh.bounds.extents.y);

      //Size of each block In Local Space
      float blockWidth = mesh.bounds.size.x / pathFinder.GridDimension.x;
      float blockHeight = mesh.bounds.size.y / pathFinder.GridDimension.y;

      for(int j = 0; j < pathFinder.GridDimension.y; j++)
      {
        for(int i = 0; i < pathFinder.GridDimension.x; i++)
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
