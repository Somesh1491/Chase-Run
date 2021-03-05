using UnityEngine;

namespace Framework
{
  public class UIToggleButton : UIButton
  {
    public bool IsOn { get { return isOn; } set { isOn = value; } }

    [SerializeField] private bool isOn;
    [SerializeField] private Color toggleColor;

    private UIToggleButtonGroup uiToggleButtonGroup;

    public override void Awake()
    {
      base.Awake();
      controller = new UIToggleButtonController(this);
      uiToggleButtonGroup = this.transform.parent.GetComponent<UIToggleButtonGroup>();
    }

    public override void OnClick()
    {
      base.OnClick();
      isOn = !isOn;

      //Inform Toggle Group if Any
      (uiToggleButtonGroup?.controller as UIToggleButtonGroupController)?.OnValueChange(this);
    }
    public virtual void SwitchOn()
    {
      Image.color = toggleColor;
    }

    public virtual void SwitchOff()
    {
      Image.color = Color.white;
    }
  }
}
