using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class Level : MonoBehaviour
  {
    private LevelData levelData;    
    private Tile[,] tiles;

    [SerializeField]
    private Tile walkablePrefab;
    [SerializeField]
    private Tile obstaclePrefab;
    private void Awake()
    {
      levelData = Resources.Load("LevelData/Level_1") as LevelData;
      tiles = new Tile[levelData.GridDimension.x, levelData.GridDimension.y];
    }

    private void Start()
    {
      for(int j = 0; j < tiles.GetLength(1); j++)
      {
        for(int i = 0; i < tiles.GetLength(0); i++)
        {
          Tile tile = null;
          if (levelData.GetTile(new Vector2Int(i, j)) == TileType.Walkable)
            tile = CreateTile(TileType.Walkable);

          else
            tile = CreateTile(TileType.Obstacle);

          tile.transform.SetParent(transform);
          tile.transform.localPosition = new Vector3(i, j, 0);
        }
      }
    }

    private Tile CreateTile(TileType tileType)
    {
      Tile tile = null;
      switch (tileType)
      {
        case TileType.Walkable:
          tile = Instantiate<Tile>(walkablePrefab);
          break;

        case TileType.Obstacle:
          tile = Instantiate<Tile>(obstaclePrefab);
          break;

        default:
          break;
      }

      return tile;
    }
  }
}
