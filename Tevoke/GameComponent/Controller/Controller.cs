namespace Framework
{
  public class Controller : CustomMonoBehaviour
  {
    public Manager manager;
    public Controller(Manager manager)
    {
      this.manager = manager;
    }
  }
}
