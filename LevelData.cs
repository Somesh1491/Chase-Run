using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChaseAndRun
{
  public class LevelData : ScriptableObject, ILevelData
  {
    public Vector2Int GridDimension { get { return gridDimension; } set { gridDimension = value; } }

    public bool IsEditingEnable { get { return isEditingEnable; } set { isEditingEnable = value; } }

    public TileType[] Tiles { get { return tiles; } set { tiles = value; } }

    public TileType SelectedTile { get { return selectedTile; } set { selectedTile = value; } }

    [SerializeField]
    private Vector2Int gridDimension;
    [SerializeField]
    private bool isEditingEnable;
    [SerializeField]
    private TileType[] tiles;
    [SerializeField]
    private TileType selectedTile;

    public void CreateTiles(Vector2Int dimension)
    {
      tiles = new TileType[dimension.x * dimension.y];
    }

    public TileType GetTile(Vector2Int index)
    {
      return tiles[gridDimension.x * index.y + index.x];
    }

    public void SetTile(Vector2Int index, TileType value)
    {
      tiles[gridDimension.x * index.y + index.x] = value;
    }
  }
}
