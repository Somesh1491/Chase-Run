using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PrefabData", order = 1)]
  public class AssetUtility : SingletonScriptableObject<AssetUtility>
  {
    public GameObject walkableTilePrefab;
    public GameObject obstacleTilePrefab;
  }
}
