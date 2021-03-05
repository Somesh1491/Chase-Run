using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
  [RequireComponent(typeof(Button))]
  public class UIButton : UIElement
  {
    public Button Button { get; private set; }
    public Image Image { get; private set; }
    public bool IsClickable { get { return isClickable; } set { isClickable = value; } }

    [SerializeField]
    private bool isClickable = true;

    public override void Awake()
    {
      base.Awake();
      controller = new UIButtonController(this);
      Button = this.GetComponent<Button>();
      Image = this.GetComponent<Image>();
    }

    public virtual void OnClick()
    {

    }
  }
}
