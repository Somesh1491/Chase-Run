using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ChaseAndRun
{
  public class LevelWindow : ILevelWindow
  {
    public float Width { get { return width; } set { width = value; } }
    public float Height { get { return height; } set { height = value; } }
    public float SplitFraction { get { return splitFraction; } set { splitFraction = value; } }

    private float width;
    private float height;
    private float splitFraction;    
  }
}
