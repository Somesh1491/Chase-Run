using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace ChaseAndRun
{
  public class LevelEditor : EditorWindow
  {
    public Vector2Int GridDimension { get { return gridDimension; } set { gridDimension = value; } }

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

    private Vector2Int gridDimension;

    private static SceneViewWindow sceneViewWindow;
    private static GUIViewWindow guiViewWindow;

    [MenuItem("Chase-Run/Open Level Editor")]
    private static void CreateWindow()
    {
      var window = GetWindow<LevelEditor>("Level Editor");
      sceneViewWindow = new SceneViewWindow(window);
      guiViewWindow = new GUIViewWindow(window);
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
      if (IsWindowResized())
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

      sceneViewWindow.DrawSceneView(splitFraction);
      guiViewWindow.DrawGUIView(splitFraction);

      Repaint();
    }

    private bool IsWindowResized()
    {
      if (position.width != windowSize.x || position.height != windowSize.y)
        return true;

      return false;
    }

    
    

    private void SetSplitter()
    {
      splitter.Set((splitFraction * windowSize.x) - 1, 0, 2f, windowSize.y);
      GUI.DrawTexture(splitter, EditorGUIUtility.whiteTexture);
      EditorGUIUtility.AddCursorRect(splitter, MouseCursor.ResizeHorizontal);
    }
  }
}
