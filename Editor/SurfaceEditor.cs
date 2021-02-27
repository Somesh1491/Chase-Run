using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ChaseAndRun
{
  [CustomEditor(typeof(Surface))]
  public class SurfaceEditor : Editor
  {
    private Surface surface;
    private Mesh mesh;
    private bool enableEditing;

    private string[] tileTypes = new string[] { "Walkable", "Obstacle" };
    private int currentTileType = 0;

    private GameObject highlightTilePrefab;
    private GameObject highlightTileObject = null;

    private void OnEnable()
    {
      surface = (Surface)target;
      if ((mesh = surface.Mesh) == null)
        Debug.LogError("No Mesh Attached to GameObject");

      highlightTilePrefab = Resources.Load("HighlightTile") as GameObject;
    }

    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();

      if (enableEditing = EditorGUILayout.Toggle("Edit Surface", enableEditing))
      {
        if (enableEditing)
        {
          currentTileType = EditorGUILayout.Popup("Tile", currentTileType, tileTypes);
        }
      }
    }

    private void OnSceneGUI()
    {
      if (enableEditing)
      {
        Event e = Event.current;

        if(e.type == EventType.MouseMove)
        {
          HighLightBlock();
        }

        else if(e.type == EventType.MouseDown)
        {
          Debug.Log(Selection.activeObject);
        }
      }

      DrawGrid();
    }

    private void HighLightBlock()
    {
      Vector2 mousePosition = Event.current.mousePosition;
      Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);

      RaycastHit hitInfo;

      if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
      {
        // if Gameobject has Surface Component
        if (hitInfo.collider.gameObject.GetComponent<Surface>() != null)
        {
          if (highlightTileObject == null)
            highlightTileObject = Instantiate(highlightTilePrefab);

          Vector2Int gridPoint = WorldToGridPoint(hitInfo.point);
          Vector3 worldPoint = GridToWorldPoint(gridPoint);

          //Size of each block In World Space
          float blockWidth = (mesh.bounds.size.x / surface.GridDimension.x) * surface.transform.localScale.x;
          float blockHeight = mesh.bounds.size.y / surface.GridDimension.y * surface.transform.localScale.y;

          highlightTileObject.transform.localScale = new Vector3(blockWidth, blockHeight, 0.1f);
          highlightTileObject.transform.position = worldPoint;
        }
      }

      else
      {
        if (highlightTileObject != null)
        {
          DestroyImmediate(highlightTileObject);
          highlightTileObject = null;
        }
      }
    }
    private Vector2Int WorldToGridPoint(Vector3 worldPoint)
    {
      //Transform Matrix
      Matrix4x4 worldToLocalMatrix = surface.transform.worldToLocalMatrix;

      //Size of surface in Local
      float surfaceWidth = mesh.bounds.size.x;
      float surfaceHeight = mesh.bounds.size.y;

      float blockCountPerUnit_X = surface.GridDimension.x / surfaceWidth;
      float blockCountPerUnit_Y = surface.GridDimension.y / surfaceHeight;

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
      Matrix4x4 localToWorldMatrix = surface.transform.localToWorldMatrix;

      //Size of each block In Local Space
      float blockWidth = mesh.bounds.size.x / surface.GridDimension.x;
      float blockHeight = mesh.bounds.size.y / surface.GridDimension.y;

      //position considering bottom left of Surface as origin In Local
      Vector3 positionInLocal = new Vector3(gridIndex.x * blockWidth, gridIndex.y * blockHeight, 0);
      //position considering center of block.
      positionInLocal += (Vector3.right * blockWidth / 2) + (Vector3.up * blockHeight / 2);
      //position considering center of surface as pivot In Local
      positionInLocal -= (Vector3.right * mesh.bounds.extents.x) + (Vector3.up * mesh.bounds.extents.y);

      //worldPosition
      return localToWorldMatrix.MultiplyPoint3x4(positionInLocal);
    }
    private void DrawGrid()
    {
      //Transform Matrix
      Matrix4x4 localToWorldMatrix = surface.transform.localToWorldMatrix;

      //Origin is consider to be bottom left of the mesh
      Vector3 originInLocal = mesh.bounds.center + (Vector3.left * mesh.bounds.extents.x) +
                                                   (Vector3.down * mesh.bounds.extents.y);

      //Size of each block In Local Space
      float blockWidth = mesh.bounds.size.x / surface.GridDimension.x;
      float blockHeight = mesh.bounds.size.y / surface.GridDimension.y;

      for (int j = 0; j < surface.GridDimension.y; j++)
      {
        for (int i = 0; i < surface.GridDimension.x; i++)
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
