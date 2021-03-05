using UnityEngine;

namespace Framework
{
  public class ObjectManager
  {
    #region Spawn Methods
    public static Pawn Instantiate(Pawn pawnPrefab)
    {
      return Object.Instantiate(pawnPrefab);
    }
    public static Pawn Instantiate(Pawn pawnPrefab, PawnContainer pawnContainer)
    {
      Pawn pawn = Object.Instantiate(pawnPrefab);
      pawnContainer.AddPawn(pawn);

      return pawn;
    }
    public static Pawn Instantiate(Pawn pawnPrefab, Transform parent)
    {
      return Object.Instantiate(pawnPrefab, parent);
    }
    public static Pawn Instantiate(Pawn pawnPrefab, Transform parent, PawnContainer pawnContainer)
    {
      Pawn pawn = Object.Instantiate(pawnPrefab, parent);
      pawnContainer.AddPawn(pawn);

      return pawn;
    }
    public static Pawn Instantiate(Pawn pawnPrefab, Vector3 position)
    {
      Pawn pawn = Object.Instantiate(pawnPrefab);
      pawn.SetPosition(position);

      return pawn;
    }
    public static Pawn Instantiate(Pawn pawnPrefab, Vector3 position, PawnContainer pawnContainer)
    {
      Pawn pawn = Object.Instantiate(pawnPrefab);
      pawnContainer.AddPawn(pawn);
      pawn.SetPosition(position);

      return pawn;
    }
    #endregion

    #region Destroy Methods
    public static void Destroy(Pawn pawn)
    {
      Object.Destroy(pawn.gameObject);
    }
    public static void Destroy(Pawn pawn, PawnContainer pawnContainer)
    {
      pawnContainer.RemovePawn(pawn);
      Object.Destroy(pawn.gameObject);
    }
    #endregion

    public static void AddPawnToContainer(Pawn pawn, PawnContainer pawnContainer)
    {
      pawnContainer.AddPawn(pawn);
    }
    public static void RemovePawnToContainer(Pawn pawn, PawnContainer pawnContainer)
    {
      pawnContainer.RemovePawn(pawn);
    }
  }
}
