using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public interface ILevelWindow
  {
    float Width { get; set; }
    float Height { get; set; }
    float SplitFraction { get; set; }
  }
}
