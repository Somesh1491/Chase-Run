using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class Level : MonoBehaviour
  {
    [SerializeField]
    private Vector2Int SourceIndex;
    [SerializeField]
    private Vector2Int TargetIndex;

    [SerializeField]
    private LevelData levelData; 
    private Tile[,] tiles;

    private ShortestPathAlgorithm<TileType> shortestPathAlgo;

    public void SetData(LevelData levelData)
    {
      this.levelData = levelData;
      tiles = new Tile[levelData.GridDimension.x, levelData.GridDimension.y];

      for (int j = 0; j < tiles.GetLength(1); j++)
      {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
          GameObject gameObject = null;
          if (levelData.GetCell(new Vector2Int(i, j)).item == TileType.Walkable)
            gameObject = CreateTileObject(TileType.Walkable);

          else
            gameObject = CreateTileObject(TileType.Obstacle);

          gameObject.transform.SetParent(transform);
          gameObject.transform.localPosition = new Vector3(i, j, 0);
        }
      }

      //Init Algo

    }

    private void Start()
    {
      shortestPathAlgo = new BreadthFirstSearch<TileType>(levelData);
      shortestPathAlgo.SourceIndex = SourceIndex;
      shortestPathAlgo.TargetIndex = TargetIndex;

      Debug.Log(levelData.Cells);

      List<Vector2Int> sp = shortestPathAlgo.ShortestPath;
      Debug.Log(sp.Count);
      foreach (var v in sp)
        Debug.Log(v);

      
    }

    private GameObject CreateTileObject(TileType tileType)
    {
      GameObject tileObject = null;

      switch (tileType)
      {
        case TileType.Walkable:
          tileObject = Instantiate(AssetUtility.Instance.walkableTilePrefab);
          break;

        case TileType.Obstacle:
          tileObject = Instantiate(AssetUtility.Instance.obstacleTilePrefab);
          break;

        default:
          break;
      }

      return tileObject;
    }
  }
}
