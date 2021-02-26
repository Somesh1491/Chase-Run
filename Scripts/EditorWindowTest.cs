using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorWindowTest : EditorWindow
{
  [MenuItem("Window/Tile View")]
 static void ShowWindow()
  {
    EditorWindow.GetWindow(typeof(EditorWindowTest));
  }

  // Window has been selected
  void OnFocus()
  {
    // Remove delegate listener if it has previously
    // been assigned.
    SceneView.duringSceneGui -= this.OnSceneGUI;
    // Add (or re-add) the delegate.
    SceneView.duringSceneGui += this.OnSceneGUI;
  }

  private void OnLostFocus()
  {
    SceneView.duringSceneGui -= this.OnSceneGUI;
  }

  void OnDestroy()
  {
    // When the window is destroyed, remove the delegate
    // so that it will no longer do any drawing.
    SceneView.duringSceneGui -= this.OnSceneGUI;
  }

  void OnSceneGUI(SceneView sceneView)
  {
    // Do your drawing here using Handles.
    Handles.BeginGUI();

    Debug.Log("Editor");
    // Do your drawing here using GUI.
    Handles.EndGUI();
  }
}
