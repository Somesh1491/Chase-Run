using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class EnemyController : MonoBehaviour
  {
    public List<Vector3> PathToTravel { get { return pathToTravel; } set { pathToTravel = value; } }
    public Vector3 targetPosition;
    public float speed = 10;

    [SerializeField]
    private List<Vector3> pathToTravel;

    public void SetEnemyPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, pathToTravel[0], speed * Time.deltaTime);    
    }
  }
}

