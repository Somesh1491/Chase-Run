using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class EnemyController : MonoBehaviour
  {
    public List<Vector3> PathToTravel { set { pathToTravel = value; } }
    public float speed = 10;

    [SerializeField]
    private List<Vector3> pathToTravel;

    public void SetPlayerPosition()
    {
      if(pathToTravel.Count > 0)
        transform.position = Vector3.MoveTowards(transform.position, pathToTravel[0], speed * Time.deltaTime);    
    }
  }
}

