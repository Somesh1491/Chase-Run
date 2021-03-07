using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace ChaseAndRun
{
  public class LevelEditorWindow : EditorWindow
  {
    //Left part of the window
    private Rect sceneView;
    //Right part of the window
    private Rect guiView;

    //Rect that divide the window in two parts
    private Rect splitter;
    private bool resizeViews = false;

    private static SceneViewWindow sceneViewWindow;
    private static GUIViewWindow guiViewWindow;
    private static ILevelEditorData levelEditorData;
    private static ILevelWindow levelWindow;

    [MenuItem("Chase-Run/Open Level Editor")]
    private static void CreateWindow()
    {
      GetWindow<LevelEditorWindow>("Level Editor");   
    }

    private void OnEnable()
    {
      levelEditorData = new LevelEditorData();
      levelWindow = new LevelWindow();

      sceneViewWindow = new SceneViewWindow(levelEditorData, levelWindow);
      guiViewWindow = new GUIViewWindow(levelEditorData, levelWindow);

      //Equal area is given to scene view and gui view in window
      levelWindow.SplitFraction = 0.5f;
      levelWindow.Width = position.width;
      levelWindow.Height = position.height;

      splitter = new Rect((levelWindow.SplitFraction * levelWindow.Width) - 1, 0, 2f, levelWindow.Height);
    }

    void OnGUI()
    {
      if (IsWindowResized())
      {
        levelWindow.Width = position.width;
        levelWindow.Height = position.height;
      }

      if (Event.current.type == EventType.MouseDown && splitter.Contains(Event.current.mousePosition))
      {
        resizeViews = true;
      }

      if (resizeViews)
      {
        levelWindow.SplitFraction = Event.current.mousePosition.x / levelWindow.Width;
      }

      if (Event.current.type == EventType.MouseUp)
        resizeViews = false;

      levelWindow.SplitFraction = Mathf.Clamp(levelWindow.SplitFraction, 0.3f, 0.7f);
      SetSplitter();

      sceneViewWindow.DrawSceneView();
      guiViewWindow.DrawGUIView();

      Repaint();
    }

    private bool IsWindowResized()
    {
      if (position.width != levelWindow.Width || position.height != levelWindow.Height)
        return true;

      return false;
    }

    private void SetSplitter()
    {
      splitter.Set((levelWindow.SplitFraction * levelWindow.Width) - 1, 0, 2f, levelWindow.Height);
      GUI.DrawTexture(splitter, EditorGUIUtility.whiteTexture);
      EditorGUIUtility.AddCursorRect(splitter, MouseCursor.ResizeHorizontal);
    }
  }
}
