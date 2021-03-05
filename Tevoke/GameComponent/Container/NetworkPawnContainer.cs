using System.Collections.Generic;

namespace Framework
{
  public class NetworkPawnContainer
  {
    protected Dictionary<int, Pawn> pawnData;

    public virtual void AddPawn(int viewID, Pawn pawn)
    {
      if (pawnData == null)
        pawnData = new Dictionary<int, Pawn>();
    }
    public virtual void RemovePawn(int viewID) { }
    public virtual Dictionary<int, Pawn> GetPawnList()
    {
      if (pawnData == null)
        pawnData = new Dictionary<int, Pawn>();

      return pawnData;
    }
  }
}
