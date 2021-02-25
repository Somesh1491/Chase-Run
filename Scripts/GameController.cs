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

    Vector3 currentMousePosition;
    bool flag = false;

    private void Update()
    {

      if(Input.GetMouseButton(0))
      {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition = mouseWorldPos;
                
        flag = true;
      }

      if (flag)
      {
        pathFinder.SourcePosition = player.position;
        pathFinder.TargetPosition = currentMousePosition;

        var a = pathFinder.ShortestPath;
        player.GetComponent<PlayerController>().PathToTravel = a;
        player.GetComponent<PlayerController>().SetPlayerPosition();

        pathFinder.SourcePosition = enemy.position;
        pathFinder.TargetPosition = player.position;

        var b = pathFinder.ShortestPath;
        enemy.GetComponent<EnemyController>().PathToTravel = b;
        enemy.GetComponent<EnemyController>().SetPlayerPosition();
      }

      

    }
  }
}
