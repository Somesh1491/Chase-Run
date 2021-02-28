using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ChaseAndRun
{
  public class GUIViewWindow
  {
    private LevelEditor window;
    private string gridDimensionTextField_X = "0";
    private string gridDimensionTextField_Y = "0";
    public GUIViewWindow(LevelEditor window)
    {
      this.window = window;
    }

    public void DrawGUIView(float splitFraction)
    {
      float x_Offset = (splitFraction * window.position.width) + 5f;
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

      Vector2Int gridDimension = Vector2Int.zero;
      if (gridDimensionTextField_X != "")
        gridDimension.x = int.Parse(gridDimensionTextField_X);
      if (gridDimensionTextField_Y != "")
        gridDimension.y = int.Parse(gridDimensionTextField_Y);

      window.GridDimension = gridDimension;

      //Execute code if mouse is in scene view or gui view
      Vector3 mousePosition = Event.current.mousePosition;
      if (mousePosition.x >= splitFraction * window.position.width && mousePosition.x <= window.position.width
          && mousePosition.y >= 0 && mousePosition.y <= (window.position.height))
        OnPointerOver();
    }

    private void OnPointerOver()
    {

    }
  }
}
