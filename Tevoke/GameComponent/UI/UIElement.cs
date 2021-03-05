namespace Framework
{
  public class UIElement : Pawn
  {
    public override void Awake()
    {
      base.Awake();
      controller = new UIElementController(this);
    }
  }
}
