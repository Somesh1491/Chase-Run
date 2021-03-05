namespace Framework
{
  public class UIScrollViewController : UIElementController
  {
    private readonly UIScrollView uiScrollView;

    public UIScrollViewController(UIElement uiElement) : base(uiElement)
    {
      uiScrollView = uiElement as UIScrollView;
    }
  }
}
