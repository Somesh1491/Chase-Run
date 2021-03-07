using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class PlayerController : MonoBehaviour
  {
    public Vector3 targetPosition;
    public float speed = 10;

    [SerializeField]
    private List<Vector3> pathToTravel;

    public void SetPlayerPosition()
    {
      if(pathToTravel.Count > 0)
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);    
    }
  }
}

