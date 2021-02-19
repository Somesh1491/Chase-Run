using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class Node<T>
  {
    public T item;
    public Node<T> parent;
  }
}
