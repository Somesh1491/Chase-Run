using UnityEngine;

namespace Framework
{
  public class SpawnUtility
  {
    #region Spawn and Destroy Methods
    public static T CreatePawn<T>(T pawnPrefab) where T : Pawn
    {
      return ObjectManager.Instantiate(pawnPrefab) as T;
    }
    public static T CreatePawn<T>(T pawnPrefab, Transform parent) where T : Pawn
    {
      return ObjectManager.Instantiate(pawnPrefab, parent) as T;
    }
    public static T CreatePawn<T, U>(T pawnPrefab) where T : Pawn where U : PawnContainer, new()
    {
      var pawnContainer = ContainerUtility.GetContainer<U>();
      return ObjectManager.Instantiate(pawnPrefab, pawnContainer) as T;
    }
    public static T CreatePawn<T, U>(T pawnPrefab, Transform parent) where T : Pawn where U : PawnContainer, new()
    {
      var pawnContainer = ContainerUtility.GetContainer<U>();
      return ObjectManager.Instantiate(pawnPrefab, parent, pawnContainer) as T;
    }
    public static void DestroyPawn(Pawn pawn)
    {
      ObjectManager.Destroy(pawn);
    }
    public static void DestroyPawn<U>(Pawn pawn) where U : PawnContainer, new()
    {
      var pawnContainer = ContainerUtility.GetContainer<U>();
      ObjectManager.Destroy(pawn, pawnContainer);
    }
    #endregion
  }
}
