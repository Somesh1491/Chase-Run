using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class EnemyController : MonoBehaviour
  {
    public Vector3 targetPosition;
    public float speed = 10;

    public void SetEnemyPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);    
    }
  }
}

