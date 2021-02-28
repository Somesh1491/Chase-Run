using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ChaseAndRun
{
  public class SceneViewWindow
  {
    private LevelEditor window;
    private float splitFraction;
    public SceneViewWindow(LevelEditor window)
    {
      this.window = window;
    }

    public void DrawSceneView(float splitFraction)
    {
      this.splitFraction = splitFraction;

      Vector2Int gridDimension = window.GridDimension;

      float sceneViewWidth = splitFraction * window.position.width;
      float sceneViewHeight = window.position.height;

      float blockWidth = sceneViewWidth / gridDimension.x;
      float blockHeight = sceneViewHeight / gridDimension.y;

      for (int j = 0; j < gridDimension.y; j++)
      {
        for (int i = 0; i < gridDimension.x; i++)
        {
          DrawRect(i * blockWidth, j * blockHeight, blockWidth, blockHeight);
        }
      }

      //Execute code if mouse is in scene view or gui view
      Vector3 mousePosition = Event.current.mousePosition;
      if (mousePosition.x >= 0 && mousePosition.x < (splitFraction * window.position.width)
          && mousePosition.y >= 0 && mousePosition.y <= (window.position.height))
        OnPointerOver();
    }

    private void OnPointerOver()
    {
      Vector2Int gridDimension = window.GridDimension;
      float sceneViewWidth = splitFraction * window.position.width;
      float sceneViewHeight = window.position.height;

      float blockWidth = sceneViewWidth / gridDimension.x;
      float blockHeight = sceneViewHeight / gridDimension.y;

      int x = (int)(Event.current.mousePosition.x / blockWidth);
      int y = (int)(Event.current.mousePosition.y / blockHeight);
    }

    private void DrawRect(float x, float y, float width, float height)
    {
      Handles.color = Color.gray;
      Vector3 point1 = new Vector3(x, y, 0);
      Vector3 point2 = new Vector3(x, y, 0) + Vector3.right * width;
      Vector3 point3 = new Vector3(x, y, 0) + Vector3.up * height;
      Vector3 point4 = point2 + (Vector3.up * height);

      Handles.DrawLine(point1, point2);
      Handles.DrawLine(point1, point3);
      Handles.DrawLine(point2, point4);
      Handles.DrawLine(point3, point4);
    }
  }
}
