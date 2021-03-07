using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ChaseAndRun
{
  public class Level : MonoBehaviour
  {
    private ILevelData levelData;

    [SerializeField]
    private Vector2Int SourceIndex;
    [SerializeField]
    private Vector2Int TargetIndex;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform enemy;
    [SerializeField]
    private TextAsset jsonLevelData;

    private void Start()
    {
      levelData = JsonConvert.DeserializeObject<LevelEditorData>(jsonLevelData.text);
      CreateLevel();

      player.localScale = transform.localScale;
      Vector2Int playerGridPos = WorldToGridPoint(player.position);
      player.transform.position = GridToWorldPoint(playerGridPos) + Vector3.right * 0.5f + Vector3.up * 0.5f;

      enemy.localScale = transform.localScale;
      Vector2Int enemyGridPos = WorldToGridPoint(enemy.position);
      enemy.transform.position = GridToWorldPoint(enemyGridPos) + Vector3.right * 0.5f + Vector3.up * 0.5f;
    }

    private void CreateLevel()
    {
      GameObject gameObject = null;
      for(int j = 0; j < levelData.tileType.GetLength(1); j++)
      {
        for(int i = 0; i < levelData.tileType.GetLength(0); i++)
        {
          GameObject gameObjectPrefab = null; 
          switch(levelData.tileType[i, j])
          {
            case TileType.Walkable:
              gameObjectPrefab = AssetUtility.Instance.walkableTilePrefab;
              gameObject = Instantiate(gameObjectPrefab);
              gameObject.transform.SetParent(transform);
              gameObject.transform.localPosition = new Vector3(i + 0.5f, j + 0.5f);
              break;

            case TileType.Obstacle:
              gameObjectPrefab = AssetUtility.Instance.obstacleTilePrefab;
              gameObject = Instantiate(gameObjectPrefab);
              gameObject.transform.SetParent(transform);
              gameObject.transform.localPosition = new Vector3(i + 0.5f, j + 0.5f);
              break;

            default:
              break;
          }
        }
      }
    }

    private void Update()
    {
      if(Input.GetMouseButtonDown(0))
      {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      }

      //Set Player Pos Auto
      if(Input.GetKeyDown(KeyCode.Space))
      {
        player.localScale = transform.localScale;
        Vector2Int playerGridPos = WorldToGridPoint(player.position);
        player.transform.position = GridToWorldPoint(playerGridPos) + Vector3.right * 0.5f * transform.localScale.x + Vector3.up * 0.5f * transform.localScale.y;

        enemy.localScale = transform.localScale;
        Vector2Int enemyGridPos = WorldToGridPoint(enemy.position);
        enemy.transform.position = GridToWorldPoint(enemyGridPos) + Vector3.right * 0.5f * transform.localScale.x + Vector3.up * 0.5f * transform.localScale.y;
      }
    }

    private Vector3 GridToWorldPoint(Vector2Int index)
    {
      Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
      return localToWorldMatrix.MultiplyPoint3x4(new Vector3(index.x, index.y));
    }

    private Vector2Int WorldToGridPoint(Vector3 worldPoint)
    {
      Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;
      Vector3 localWorldPoint = worldToLocalMatrix.MultiplyPoint3x4(worldPoint);

      return new Vector2Int((int)localWorldPoint.x, (int)localWorldPoint.y);
    }
  } 
}
