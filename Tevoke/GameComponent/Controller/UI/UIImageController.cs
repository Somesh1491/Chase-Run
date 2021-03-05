namespace Framework
{
  public class UIImageController : UIElementController
  {
    private readonly UIImage uiImage;

    public UIImageController(UIElement uiElement) : base(uiElement)
    {
      uiImage = uiElement as UIImage;
    }
  }
}
