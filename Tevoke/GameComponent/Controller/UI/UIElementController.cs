namespace Framework
{
  public class UIElementController : PawnController
  {
    private readonly UIElement uiElement;
    public UIElementController(Pawn pawn) : base(pawn)
    {
      uiElement = pawn as UIElement;
    }
  }
}
