using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class Tile : MonoBehaviour
  {
    public TileType Type { get { return type; } }
    
    [SerializeField]
    private TileType type;
        
  }
}
