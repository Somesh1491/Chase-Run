using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class GameController : MonoBehaviour
  {
    public Transform player;
    public Transform enemy;
    public PathFinder pathFinder;
    
    private void Update()
    {
      pathFinder.SourcePosition = player.position;
      pathFinder.TargetPosition = enemy.position;

      var a = pathFinder.ShortestPath;
    }
  }
}
