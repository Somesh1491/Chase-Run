using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class Utility
  {
    /// <summary>
    /// Return the mid way position between from and to.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="fractionTravel">amount of journey completed (must be between 0-1)</param>
    public static Vector3 GetIntermediateVector(Vector3 from, Vector3 to, float fractionTravel)
    {
      return ((1 - fractionTravel) * from) + (fractionTravel * to);
    }

    public static float ProjectionOverVector(Vector3 vectorToProject, Vector3 vectorOverProject)
    {
      vectorOverProject = vectorOverProject.normalized;
      float projectionAmt = Vector3.Dot(vectorToProject, vectorOverProject);

      return projectionAmt;
    }
  }
}
