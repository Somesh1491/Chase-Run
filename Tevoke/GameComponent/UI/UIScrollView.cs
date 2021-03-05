using UnityEngine.UIElements;

namespace Framework
{
  public class UIScrollView : UIElement
  {
    public ScrollView ScrollView { get; private set; }

    public override void Awake()
    {
      base.Awake();
    }
  }
}
