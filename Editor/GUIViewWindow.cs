using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace ChaseAndRun
{
  public class GUIViewWindow
  {
    private ILevelWindow levelWindow;
    private ILevelEditorData levelEditorData;
    private string gridDimensionTextField_X = "0";
    private string gridDimensionTextField_Y = "0";
    private string[] tileTypes = new string[] { "Walkable", "Obstacle" };
    public GUIViewWindow(ILevelEditorData levelEditorData, ILevelWindow levelWindow)
    {
      this.levelEditorData = levelEditorData;
      this.levelWindow = levelWindow;
    }

    public void DrawGUIView()
    {
      float splitFraction = levelWindow.SplitFraction;
      float window_width = levelWindow.Width;
      float window_height = levelWindow.Height;

      float x_Offset = (splitFraction * window_width) + 5f;
      Rect gridDimensionLabelRect = new Rect(x_Offset, 5, 100, 20);
      EditorGUI.LabelField(gridDimensionLabelRect, "Grid Dimension");

      x_Offset += gridDimensionLabelRect.width + 10f;
      Rect gridDimensionX_TextField_Rect = new Rect(x_Offset, 5, 100, 20);
      gridDimensionTextField_X = EditorGUI.TextField(gridDimensionX_TextField_Rect, gridDimensionTextField_X);
      gridDimensionTextField_X = Regex.Replace(gridDimensionTextField_X, @"[^0-9]", "");

      x_Offset += gridDimensionX_TextField_Rect.width + 10f;
      Rect gridDimensionY_TextField_Rect = new Rect(x_Offset, 5, 100, 20);
      gridDimensionTextField_Y = EditorGUI.TextField(gridDimensionY_TextField_Rect, gridDimensionTextField_Y);
      gridDimensionTextField_Y = Regex.Replace(gridDimensionTextField_Y, @"[^0-9]", "");

      Vector2Int gridDimension = Vector2Int.zero;
      if (gridDimensionTextField_X != "")
        gridDimension.x = int.Parse(gridDimensionTextField_X);
      if (gridDimensionTextField_Y != "")
        gridDimension.y = int.Parse(gridDimensionTextField_Y);

      //Draw Tile options
      x_Offset = (splitFraction * window_width) + 5f;

      Rect gridDimensionPopUpLabelRect = new Rect(x_Offset, 40, 100, 20);
      EditorGUI.LabelField(gridDimensionPopUpLabelRect, "Tile Type");

      x_Offset += gridDimensionX_TextField_Rect.width + 10f;

      Rect gridDimensionPopUpRect = new Rect(x_Offset, 40, 100, 20);
      int currentTile = EditorGUI.Popup(gridDimensionPopUpRect, (int)levelEditorData.SelectedTile, tileTypes);
      levelEditorData.SelectedTile = (TileType)currentTile;

      //Draw Edit and Save Level
      x_Offset = (splitFraction * window_width) + 5f;
      Rect editButtonRect = new Rect(x_Offset, 60, 100, 20);
      if(GUI.Button(editButtonRect, "Edit Level"))
      {
        levelEditorData.IsEditingEnable = true;
        levelEditorData.Cells = new Cell[gridDimension.x, gridDimension.y];
        levelEditorData.tileType = new TileType[gridDimension.x, gridDimension.y];
      }

      x_Offset += gridDimensionX_TextField_Rect.width + 10f;
      Rect saveButtonRect = new Rect(x_Offset, 60, 100, 20);
      if (GUI.Button(saveButtonRect, "Save Level"))
      {
        string jsonLevelData = JsonConvert.SerializeObject(levelEditorData);

        TextAsset textAsset = new TextAsset(jsonLevelData);

        //using (FileStream fs = new FileStream("Assets/Resources/LevelData/LevelData.json", FileMode.Create))
        //{
        //  using (StreamWriter writer = new StreamWriter(fs))
        //  {
        //    writer.Write(jsonLevelData);
        //  }
        //}

        GameObject levelObj = new GameObject("Level");
        levelObj.AddComponent<Level>().CreateLevel(levelEditorData);
        levelObj.GetComponent<Level>().jsonLevelData = textAsset;

        UnityEditor.AssetDatabase.Refresh();
      }

      if (levelEditorData.GridDimension.x != gridDimension.x || levelEditorData.GridDimension.y != gridDimension.y)
      {
        levelEditorData.GridDimension = gridDimension;
      }

      //Execute code if mouse is in scene view or gui view
      Vector3 mousePosition = Event.current.mousePosition;
      if (mousePosition.x >= splitFraction * window_width && mousePosition.x <= window_width
          && mousePosition.y >= 0 && mousePosition.y <= (window_height))
        OnPointerOver();
    }

    private void OnPointerOver()
    {

    }
  }
}
