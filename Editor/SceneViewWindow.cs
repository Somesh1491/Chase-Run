using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ChaseAndRun
{
  public class SceneViewWindow
  {
    private ILevelEditorData levelEditorData;
    private ILevelWindow levelWindow;

    public SceneViewWindow(ILevelEditorData levelEditorData, ILevelWindow levelWindow)
    {
      this.levelEditorData = levelEditorData;
      this.levelWindow = levelWindow;
    }

    public void DrawSceneView()
    {
      //Draw Wire Frame
      float window_width = levelWindow.Width;
      float window_height = levelWindow.Height;
      float splitFraction = levelWindow.SplitFraction;

      Vector2Int gridDimension = levelEditorData.GridDimension;

      float sceneViewWidth = splitFraction * window_width;
      float sceneViewHeight = window_height;

      float blockWidth = sceneViewWidth / gridDimension.x;
      float blockHeight = sceneViewHeight / gridDimension.y;

      for (int j = 0; j < gridDimension.y; j++)
      {
        for (int i = 0; i < gridDimension.x; i++)
        {
          Handles.color = Color.gray;
          DrawRect(i * blockWidth, j * blockHeight, blockWidth, blockHeight);
        }
      }

      //Draw Tiles
      if (levelEditorData.Cells != null)
      {
        for (int i = 0; i < levelEditorData.Cells.Length; i++)
        {

          int y = i / gridDimension.x;
          int x = i % gridDimension.x;

          Vector3 tileScreenPosition = GridToScreenPoint(new Vector2Int(x, y));
          switch (levelEditorData.tileType[x, y])
          {
            case TileType.Walkable:
              Handles.DrawSolidRectangleWithOutline(new Rect(tileScreenPosition.x, tileScreenPosition.y, blockWidth, blockHeight), Color.red, Color.black);
              break;
            case TileType.Obstacle:
              Handles.DrawSolidRectangleWithOutline(new Rect(tileScreenPosition.x, tileScreenPosition.y, blockWidth, blockHeight), Color.gray, Color.black);
              break;
            default:
              break;
          }
        }
      }

      //Execute code if mouse is in scene view or gui view
      Vector3 mousePosition = Event.current.mousePosition;
      if (mousePosition.x >= 0 && mousePosition.x < (splitFraction * window_width)
          && mousePosition.y >= 0 && mousePosition.y <= (window_height))
        OnPointerOver();
    }

    private void OnPointerOver()
    {
      if(levelEditorData.IsEditingEnable)
      {
        Vector2Int gridDimension = levelEditorData.GridDimension;
        float sceneViewWidth = levelWindow.SplitFraction * levelWindow.Width;
        float sceneViewHeight = levelWindow.Height;

        float blockWidth = sceneViewWidth / gridDimension.x;
        float blockHeight = sceneViewHeight / gridDimension.y;

        Vector2Int gridPosition = ScreenToGridPoint(Event.current.mousePosition);
        Vector3 screenPosition = GridToScreenPoint(gridPosition);

        if(levelEditorData.SelectedTile == TileType.Walkable)
          Handles.DrawSolidRectangleWithOutline(new Rect(screenPosition.x, screenPosition.y, blockWidth, blockHeight), Color.red, Color.black);

        else if(levelEditorData.SelectedTile == TileType.Obstacle)
          Handles.DrawSolidRectangleWithOutline(new Rect(screenPosition.x, screenPosition.y, blockWidth, blockHeight), Color.gray, Color.black);

        //If Mouse Click over grid
        if(Event.current.type == EventType.MouseUp)
        {
          Vector2Int gridPoint = ScreenToGridPoint(Event.current.mousePosition);
          levelEditorData.tileType[gridPoint.x, gridPoint.y] = levelEditorData.SelectedTile;

          switch (levelEditorData.SelectedTile)
          {
            case TileType.Walkable:
              levelEditorData.Cells[gridPoint.x, gridPoint.y].isBlocked = false;
              break;
            case TileType.Obstacle:
              levelEditorData.Cells[gridPoint.x, gridPoint.y].isBlocked = true;
              break;
            default:
              break;
          }
        }
      }
    }

    private Vector2Int ScreenToGridPoint(Vector3 screenPoint)
    {
      Vector2Int gridDimension = levelEditorData.GridDimension;
      float sceneViewWidth = levelWindow.SplitFraction * levelWindow.Width;
      float sceneViewHeight = levelWindow.Height;

      float blockWidth = sceneViewWidth / gridDimension.x;
      float blockHeight = sceneViewHeight / gridDimension.y;

      int x = (int)(screenPoint.x / blockWidth);
      //make the origin from top left to bottom left
      int y = (gridDimension.y - 1) - (int)(screenPoint.y / blockHeight);

      return new Vector2Int(x, y);
    }

    private Vector3 GridToScreenPoint(Vector2Int gridPoint)
    {
      gridPoint.y = (levelEditorData.GridDimension.y - 1) - gridPoint.y;
      Vector2Int gridDimension = levelEditorData.GridDimension;
      float sceneViewWidth = levelWindow.SplitFraction * levelWindow.Width;
      float sceneViewHeight = levelWindow.Height;

      float blockWidth = sceneViewWidth / gridDimension.x;
      float blockHeight = sceneViewHeight / gridDimension.y;

      Vector3 screenPoint = new Vector3(gridPoint.x * blockWidth, gridPoint.y * blockHeight);

      return screenPoint;
    }
    private void DrawRect(float x, float y, float width, float height)
    {
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
