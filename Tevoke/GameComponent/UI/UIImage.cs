using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
  [RequireComponent(typeof(Image))]
  public class UIImage : UIElement
  {
    public Image Image { get; private set; }

    public override void Awake()
    {
      base.Awake();
      controller = new UIImageController(this);
      Image = this.GetComponent<Image>();
    }
  }
}
