using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
  //Left part of the window
  private Rect sceneView;
  //Right part of the window
  private Rect guiView;

  private Vector2 windowSize;
  //Rect that divide the window in two parts
  private Rect splitter;
  //tell about the scene view and gui view space ratio in window
  private float splitFraction;
  private bool resizeViews = false;

  private string gridDimensionTextField_X = "0";
  private string gridDimensionTextField_Y = "0";
  private Vector2Int gridDimension;

  [MenuItem("Chase-Run/Open Level Editor")]
  private static void CreateWindow()
  {
    GetWindow<LevelEditor>("Level Editor");
  }

  private void OnEnable()
  {
    windowSize = position.size;
    //Equal area is given to scene view and gui view in window
    splitFraction = 0.5f;

    splitter = new Rect((splitFraction * windowSize.x) - 1, 0, 2f, windowSize.y);
  }

  void OnGUI()
  {
    if(IsWindowResized())
    {
      windowSize = position.size;
    }

    if (Event.current.type == EventType.MouseDown && splitter.Contains(Event.current.mousePosition))
    {
      resizeViews = true;
    }

    if (resizeViews)
    {
      splitFraction = Event.current.mousePosition.x / windowSize.x;
    }

    if (Event.current.type == EventType.MouseUp)
      resizeViews = false;

    splitFraction = Mathf.Clamp(splitFraction, 0.3f, 0.7f);
    SetSplitter();

    DrawSceneView();
    DrawGUIView();

    //Execute code if mouse is in scene view or gui view
    if (Event.current.mousePosition.x <= (splitFraction * windowSize.x))
      PointerOnSceneView();
    else
      PointerOnGUIView();
    Repaint();
  }

  private bool IsWindowResized()
  {
    if (position.width != windowSize.x || position.height != windowSize.y)
      return true;

    return false;
  }

  private void DrawSceneView()
  {

  }

  private void DrawGUIView()
  {
    float x_Offset = (splitFraction * windowSize.x) + 5f;
    Rect gridDimensionLabelRect = new Rect(x_Offset, 5, 100, 20);
    GUI.Label(gridDimensionLabelRect, "Grid Dimension");

    x_Offset += gridDimensionLabelRect.width + 10f;
    Rect gridDimensionX_TextField_Rect = new Rect(x_Offset, 5, 100, 20);
    gridDimensionTextField_X = GUI.TextField(gridDimensionX_TextField_Rect, gridDimensionTextField_X);
    gridDimensionTextField_X = Regex.Replace(gridDimensionTextField_X, @"[^0-9]", "");

    x_Offset += gridDimensionX_TextField_Rect.width + 10f;
    Rect gridDimensionY_TextField_Rect = new Rect(x_Offset, 5, 100, 20);
    gridDimensionTextField_Y = GUI.TextField(gridDimensionY_TextField_Rect, gridDimensionTextField_Y);
    gridDimensionTextField_Y = Regex.Replace(gridDimensionTextField_Y, @"[^0-9]", "");

    if(gridDimensionTextField_X != "")
      gridDimension.x = int.Parse(gridDimensionTextField_X);
    if(gridDimensionTextField_Y != "")
    gridDimension.y = int.Parse(gridDimensionTextField_Y);
  }

  private void PointerOnSceneView()
  {
    
  }

  private void PointerOnGUIView()
  {
    
  }

  private void SetSplitter()
  {
    splitter.Set((splitFraction * windowSize.x) - 1, 0, 2f, windowSize.y);
    GUI.DrawTexture(splitter, EditorGUIUtility.whiteTexture);
    EditorGUIUtility.AddCursorRect(splitter, MouseCursor.ResizeHorizontal);
  }
}
