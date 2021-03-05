namespace Framework
{
  public class PawnContainer : Container
  {
    public object dataStructure;

    public void CreateDataStructure<T>() where T : DataStructure, new()
    {
      dataStructure = new T();
    }
    public virtual void AddPawn(Pawn pawn)
    {
    }

    public virtual void RemovePawn(Pawn pawn)
    {
    }

    public virtual T GetPawnData<T>() where T : DataStructure
    {
      return dataStructure as T;
    }
  }
}
