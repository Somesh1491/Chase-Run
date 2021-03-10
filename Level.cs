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
    public TextAsset jsonLevelData;

    private bool isGameStarted = false;
    Vector3 currentMousePosition;

    private ShortestPathAlgorithm shortestPathAlgorithm;

    private void Start()
    {
      levelData = JsonConvert.DeserializeObject<LevelEditorData>(jsonLevelData.text);

      player.localScale = transform.localScale;
      Vector2Int playerGridPos = WorldToGridPoint(player.position);
      player.transform.position = GridToWorldPoint(playerGridPos);

      enemy.localScale = transform.localScale;
      Vector2Int enemyGridPos = WorldToGridPoint(enemy.position);
      enemy.transform.position = GridToWorldPoint(enemyGridPos);

      shortestPathAlgorithm = new BreadthFirstSearch(levelData);
    }


    public void CreateLevel(ILevelData levelData)
    {
      this.levelData = levelData;
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

      if (Input.GetMouseButton(0))
      {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition = mouseWorldPos;

        isGameStarted = true;
      }

      if (isGameStarted)
      {
        shortestPathAlgorithm.SourceIndex = WorldToGridPoint(player.transform.position);
        shortestPathAlgorithm.TargetIndex = WorldToGridPoint(currentMousePosition);

        var a = shortestPathAlgorithm.ShortestPath;

        player.GetComponent<PlayerController>().PathToTravel = GetWorldPoint(a);
        player.GetComponent<PlayerController>().SetPlayerPosition();
        player.GetComponent<LineRenderer>().positionCount = GetWorldPoint(a).Count;
        player.GetComponent<LineRenderer>().SetPositions(GetWorldPoint(a).ToArray());

        shortestPathAlgorithm.SourceIndex = WorldToGridPoint(enemy.transform.position);
        shortestPathAlgorithm.TargetIndex = WorldToGridPoint(player.transform.position);

        var b = shortestPathAlgorithm.ShortestPath;
        enemy.GetComponent<EnemyController>().PathToTravel = GetWorldPoint(b);
        enemy.GetComponent<EnemyController>().SetEnemyPosition();
        enemy.GetComponent<LineRenderer>().positionCount = GetWorldPoint(b).Count;
        enemy.GetComponent<LineRenderer>().SetPositions(GetWorldPoint(b).ToArray());
      }
    }

      private Vector3 GridToWorldPoint(Vector2Int index)
    {
      Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
      return localToWorldMatrix.MultiplyPoint3x4(new Vector3(index.x, index.y)) + Vector3.right * 0.5f * transform.localScale.x + Vector3.up * 0.5f * transform.localScale.y;
    }

    private Vector2Int WorldToGridPoint(Vector3 worldPoint)
    {
      Matrix4x4 worldToLocalMatrix = transform.worldToLocalMatrix;
      Vector3 localWorldPoint = worldToLocalMatrix.MultiplyPoint3x4(worldPoint);

      return new Vector2Int((int)localWorldPoint.x, (int)localWorldPoint.y);
    }

    private List<Vector3> GetWorldPoint(List<Vector2Int> gridPoint)
    {
      List<Vector3> worldPoints = new List<Vector3>();
      foreach(var point in gridPoint)
      {
        worldPoints.Add(GridToWorldPoint(point));
      }

      return worldPoints;
    }
  } 
}
