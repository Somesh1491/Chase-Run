using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class Level : MonoBehaviour
  {
    private IGrid<TileType> gridData;    
    private Tile[,] tiles;

    public void SetData(IGrid<TileType> gridData)
    {
      this.gridData = gridData;
      tiles = new Tile[gridData.GridDimension.x, gridData.GridDimension.y];

      for (int j = 0; j < tiles.GetLength(1); j++)
      {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
          GameObject gameObject = null;
          if (gridData.GetCell(new Vector2Int(i, j)).item == TileType.Walkable)
            gameObject = CreateTileObject(TileType.Walkable);

          else
            gameObject = CreateTileObject(TileType.Obstacle);

          gameObject.transform.SetParent(transform);
          gameObject.transform.localPosition = new Vector3(i, j, 0);
        }
      }
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
